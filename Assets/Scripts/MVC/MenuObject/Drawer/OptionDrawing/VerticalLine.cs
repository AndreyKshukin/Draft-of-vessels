using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLine : Drawing
{
    //public VerticalLine(Texture2D texture2D, Color color) : base(texture2D, color)
    //{
    //}

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Проверяет, кликает ли мышка пообъекту.
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, _drawingLayer.value);
            if (hit != null && hit.transform != null)
            {
                VerticalLineBrush(mouseWorldPosition);
            }
        }
    }

    private void VerticalLineBrush(Vector2 world_point)
    {
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);
        _curColors = _texture2D.GetPixels32();

        VerticalLineMarkPixelsToColor(pixel_pos, _widthLine, _colorLine);

        ApplyMarkedPixelChanges();
    }

    private void VerticalLineMarkPixelsToColor(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;
        int extra_radius = Mathf.Min(0, pen_thickness - 2);

        for (int x = Mathf.Max(center_x - pen_thickness, 1); x <= center_x + pen_thickness; x++)
        {

            if (x >= (int)_sprite.rect.width
                || x < 0)
                continue;

            for (int y = center_y; y <= _texture2D.height; y++)
            {
                if(_texture2D.GetPixel(x,y) != Color.black)
                    MarkPixelToChange(x, y, color_of_pen);
                else
                {
                    y = _texture2D.height;
                }
            }

            for (int y = center_y - 1; y >= 0; y--)
            {
                if (_texture2D.GetPixel(x, y) != Color.black)
                    MarkPixelToChange(x, y, color_of_pen);
                else
                {
                    y = 0;
                }
            }
        }
    }
}
