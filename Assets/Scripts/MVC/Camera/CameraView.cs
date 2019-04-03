using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : DovElement
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            App.Notify(CameraNotifiction.TransformPosCamera, this, pos);
            Debug.Log(pos);
        }
    }

    public void UpButton()
    {
        App.Notify(CameraNotifiction.UpCamera, this);
    }

    public void DownButton()
    {
        App.Notify(CameraNotifiction.DownCamera, this);
    }

    public void LeftButton()
    {
        App.Notify(CameraNotifiction.LeftCamera, this);
    }

    public void RightButton()
    {
        App.Notify(CameraNotifiction.RightCamera, this);
    }

    public void TurnLeftButton()
    {
        App.Notify(CameraNotifiction.TurnLeftCamera, this);
    }

    public void TurnRightButton()
    {
        App.Notify(CameraNotifiction.TurnRightCamera, this);
    }

    public void CenterButton()
    {
        App.Notify(CameraNotifiction.CenterPosCamera, this);
    }
}
