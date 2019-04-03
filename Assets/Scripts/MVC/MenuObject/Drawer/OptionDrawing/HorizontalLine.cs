using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : Drawing
{
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

        for (int x = center_x; x <= _texture2D.width - 1; x++)
        {

            if (x >= (int)_sprite.rect.width
                || x < 0)
                continue;

            for (int y = Mathf.Max(center_y - pen_thickness, 1); y <= center_y + pen_thickness; y++)
            {
                if (_texture2D.GetPixel(x, y) != Color.black)
                    MarkPixelToChange(x, y, color_of_pen);
                else
                {
                    x = _texture2D.width;
                }
            }
        }

        for (int x = center_x - 1; x >= 1; x--)
        {

            if (x >= (int)_sprite.rect.width
                || x < 0)
                continue;

            for (int y = Mathf.Max(center_y - pen_thickness, 1); y <= center_y + pen_thickness; y++)
            {
                if (_texture2D.GetPixel(x, y) != Color.black)
                    MarkPixelToChange(x, y, color_of_pen);
                else
                {
                    x = 0;
                }
            }
        }
    }
}
