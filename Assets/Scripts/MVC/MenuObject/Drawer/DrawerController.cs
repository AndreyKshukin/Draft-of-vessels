using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class DrawerController : DovController
{
    private DrawerModel _drModel => App.Model.DrawerModel;
    private Texture2D _tempTexture2D;

    public override void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case (DrawerNotifiction.OpenDrawer):
                _drModel.DrawerView.SetActive(true);
                App.Model.ModelsVeiw.SetActive(false);
                Texture2D tex = (Texture2D) App.Model.Element.Model_3D.GetComponent<Renderer>().material.mainTexture;

                if (tex != null)
                    Graphics.CopyTexture(tex, _tempTexture2D);
                else
                {
                    _tempTexture2D = new Texture2D(
                        Mathf.CeilToInt((float) (App.Model.Element.Model_3D.transform.localScale.z / 10 * 3.14)),
                        (int) App.Model.Element.Model_3D.transform.localScale.x / 10);

                    for (int y = 0; y < _tempTexture2D.height; y++)
                    {
                        for (int x = 0; x < _tempTexture2D.width; x++)
                        {
                            Color color = Color.white;
                            _tempTexture2D.SetPixel(x, y, color);
                        }
                    }

                    _tempTexture2D.Apply();
                }

                _drModel.DrawingCanvas.GetComponent<BoxCollider2D>().size = 
                    new Vector2(System.Convert.ToSingle(_tempTexture2D.width) / 100, System.Convert.ToSingle(_tempTexture2D.height) / 100);

                _drModel.DrawingCanvas.GetComponent<SpriteRenderer>().sprite = 
                    Sprite.Create(_tempTexture2D, new Rect(0.0f, 0.0f, _tempTexture2D.width, _tempTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);

                _drModel.SpriteObject = _drModel.DrawingCanvas.GetComponent<SpriteRenderer>().sprite;

                _drModel.Texture2DObject = new Texture2D(_tempTexture2D.width, _tempTexture2D.height);
                    Graphics.CopyTexture(_tempTexture2D, _drModel.Texture2DObject);

                _drModel.MenuDrawer.SetActive(true);
                _drModel.ButtonsPicture.SetActive(false);
                break;

            case (DrawerNotifiction.ApplyDrawer):
                _drModel.DrawerView.SetActive(false);
                App.Model.ModelsVeiw.SetActive(true);
                App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                App.Model.Element.Model_3D.GetComponent<Renderer>().material.mainTexture = _drModel.Texture2DObject;
                break;

            case (DrawerNotifiction.CancelDrawer):
                _drModel.DrawerView.SetActive(false);
                App.Model.ModelsVeiw.SetActive(true);
                App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                break;

            case (DrawerNotifiction.FillWhiteDrawer):
                FillWhite fill = new FillWhite();
                fill.Fill(ref _drModel.Texture2DObject);
                fill.Fill(ref _tempTexture2D);
                break;

            case (DrawerNotifiction.PenDrawer):
                _drModel.DrawingCanvas.AddComponent<PenBrush>();
                _drModel.DrawingCanvas.GetComponent<PenBrush>().AddCompanent(_tempTexture2D, Color.black, _drModel.SpriteObject);
                _drModel.MenuDrawer.SetActive(false);
                _drModel.ButtonsPicture.SetActive(true);
                break;

            case (DrawerNotifiction.EraserDrawer):
                _drModel.DrawingCanvas.AddComponent<PenBrush>();
                _drModel.DrawingCanvas.GetComponent<PenBrush>().AddCompanent(_tempTexture2D, Color.white, _drModel.SpriteObject);
                _drModel.MenuDrawer.SetActive(false);
                _drModel.ButtonsPicture.SetActive(true);
                break;

            case (DrawerNotifiction.VerticalDrawer):
                _drModel.DrawingCanvas.AddComponent<VerticalLine>();
                _drModel.DrawingCanvas.GetComponent<VerticalLine>().AddCompanent(_tempTexture2D, Color.black, _drModel.SpriteObject);
                _drModel.MenuDrawer.SetActive(false);
                _drModel.ButtonsPicture.SetActive(true);
                break;

            case (DrawerNotifiction.HorizontalDrawer):
                _drModel.DrawingCanvas.AddComponent<HorizontalLine>();
                _drModel.DrawingCanvas.GetComponent<HorizontalLine>().AddCompanent(_tempTexture2D, Color.black, _drModel.SpriteObject);
                _drModel.MenuDrawer.SetActive(false);
                _drModel.ButtonsPicture.SetActive(true);
                break;

            case (DrawerNotifiction.PictureApplyDrawer):
                _drModel.Texture2DObject = new Texture2D(_tempTexture2D.width, _tempTexture2D.height);
                Graphics.CopyTexture(_tempTexture2D, _drModel.Texture2DObject);
                Destroy(_drModel.DrawingCanvas.GetComponent<Drawing>());
                _drModel.MenuDrawer.SetActive(true);
                _drModel.ButtonsPicture.SetActive(false);
                break;

            case (DrawerNotifiction.PictureCancelDrawer):
               Color[] pixelBuffer = _drModel.Texture2DObject.GetPixels();
                _tempTexture2D.SetPixels(pixelBuffer);

                Graphics.CopyTexture(_drModel.Texture2DObject, _tempTexture2D);
                _tempTexture2D.Apply();

                Destroy(_drModel.DrawingCanvas.GetComponent<Drawing>());
                _drModel.MenuDrawer.SetActive(true);
                _drModel.ButtonsPicture.SetActive(false);
                break;
            default:
                break;
        }
    }
}
