using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class DrawerChange : MonoBehaviour
{
    public Color ColorLine = Color.black;
    public int WidthLine = 3;

    private Sprite _sprite;
    private Texture2D _texture2D;

    public delegate void BrushFunction(Vector2 world_position);
    public BrushFunction CurrentBrush;

    public LayerMask DrawingLayers;

    private Vector2 _previousDragPosition;     // передыдущая позиция
    private Color32[] _curColors;              // Переменная нужна для быстрого сохранения.

    public GameObject Obj;

    public Texture Tex;
    public void Test()
    {
        _texture2D =  new Texture2D(1000, 800);

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(_texture2D.width / 100, _texture2D.height / 100);

        GetComponent<SpriteRenderer>().sprite = Sprite.Create(_texture2D, new Rect(0.0f, 0.0f, _texture2D.width, _texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        _sprite = GetComponent<SpriteRenderer>().sprite;

        for (int y = 0; y < _texture2D.height; y++)
        {
            for (int x = 0; x < _texture2D.width; x++)
            {
                Color color = Color.white;
                _texture2D.SetPixel(x, y, color);
            }
        }
        _texture2D.Apply();
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
            Apply();

        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Проверяет, кликает ли мышка пообъекту.
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, DrawingLayers.value);
            if (hit != null && hit.transform != null)
            {
                PenBrush(mouseWorldPosition);
            }
        }

        else
        {
            _previousDragPosition = Vector2.zero;
        }
    }



    // Конвертация мировых координат в координаты изображения
    public Vector2 WorldToPixelCoordinates(Vector2 world_position)
    {
        Vector3 local_pos = transform.InverseTransformPoint(world_position);

        float pixelWidth = _sprite.rect.width;
        float pixelHeight = _sprite.rect.height;
        float unitsToPixels = pixelWidth / _sprite.bounds.size.x * transform.localScale.x;

        float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
        float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

        Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

        return pixel_pos;
    }

    // Выбор точек которые должны закрашиваться
    //////////////////////////////////////////////////////////////////////////

    // квадратная кисть,расскрашивает прямые между точками.
    public void ColorBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
    {
        float distance = Vector2.Distance(start_point, end_point);
        Vector2 direction = (start_point - end_point).normalized;

        Vector2 cur_position = start_point;

        float lerp_steps = 1 / distance;

        for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
        {
            cur_position = Vector2.Lerp(start_point, end_point, lerp);
            MarkPixelsToColor(cur_position, width, color);
        }
    }
    //Раскрашивает точки
    public void MarkPixelsToColor(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;
        int extra_radius = Mathf.Min(0, pen_thickness - 2);

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {
            if (x >= (int)_sprite.rect.width
                || x < 0)
                continue;

            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                MarkPixelToChange(x, y, color_of_pen);
            }
        }
    }
    // конец квадратной кисти


    public void VerticalLineMarkPixelsToColor(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;
        int extra_radius = Mathf.Min(0, pen_thickness - 2);

        for (int x = Mathf.Max(center_x - pen_thickness, 1) ; x <= center_x + pen_thickness; x++)
        {

            if (x >= (int)_sprite.rect.width
                || x < 0)
                continue;

            for (int y = 0; y <= _texture2D.height; y++)
            {
                MarkPixelToChange(x, y, color_of_pen);
            }
        }
    }

    // Смена цветов в пикселях
    public void MarkPixelToChange(int x, int y, Color color)
    {
        int array_pos = y * (int)_sprite.rect.width + x;

        if (array_pos > _curColors.Length || array_pos < 0)
            return;

        _curColors[array_pos] = color;
    }

    public void ApplyMarkedPixelChanges()
    {
        _texture2D.SetPixels32(_curColors);
        _texture2D.Apply();
    }



    //Кисти
    //////////////////////////////////////////////////////////////////////
    public void PenBrush(Vector2 world_point)
    {
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

        _curColors = _texture2D.GetPixels32();

        if (_previousDragPosition == Vector2.zero)
        {
            MarkPixelsToColor(pixel_pos, WidthLine, ColorLine);
        }
        else
        {
            ColorBetween(_previousDragPosition, pixel_pos, WidthLine, ColorLine);
        }

        ApplyMarkedPixelChanges();

        _previousDragPosition = pixel_pos;
    }

    public void VerticalLineBrush(Vector2 world_point)
    {
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);
        _curColors = _texture2D.GetPixels32();

        VerticalLineMarkPixelsToColor(pixel_pos, WidthLine, ColorLine);

        ApplyMarkedPixelChanges();
    }

    private void Apply()
    {
        Debug.Log("Check");
        Tex = _texture2D;
        Obj.GetComponent<Renderer>().material.mainTexture = Tex;
    }
}
    
