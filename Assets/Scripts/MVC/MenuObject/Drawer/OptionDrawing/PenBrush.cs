using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenBrush : Drawing
{
    private Vector2 _previousDragPosition;

    //public PenBrush(Texture2D texture2D, Color color) : base(texture2D, color)
    //{
    //}

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Проверяет, кликает ли мышка пообъекту.
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, _drawingLayer.value);
            if (hit != null && hit.transform != null)
            {
                Brush(mouseWorldPosition);
            }
        }

        else
        {
            _previousDragPosition = Vector2.zero;
        }
    }


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

    //Раскрашивает точки по квадрату
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


    public void Brush(Vector2 world_point)
    {
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

        _curColors = _texture2D.GetPixels32();

        if (_previousDragPosition == Vector2.zero)
        {
            MarkPixelsToColor(pixel_pos, _widthLine, _colorLine);
        }

        else
        {
            ColorBetween(_previousDragPosition, pixel_pos, _widthLine, _colorLine);
        }

        ApplyMarkedPixelChanges();

        _previousDragPosition = pixel_pos;
    }
}
