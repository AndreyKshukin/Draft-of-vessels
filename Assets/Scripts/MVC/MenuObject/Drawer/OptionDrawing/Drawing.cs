using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Конвектирует мировые координаты в координаты изображения.
/// Смена цветов в пикселях.
/// </summary>
public class Drawing : MonoBehaviour
{
    protected int _widthLine;
    protected Color _colorLine;

    protected LayerMask _drawingLayer = 256;
    protected Texture2D _texture2D;
    private Texture2D _memoryTextere;
    protected Sprite _sprite;
    protected Color32[] _curColors;

    //public Drawing(Texture2D texture2D, Color color)
    //{
    //    _texture2D = texture2D;
    //    _colorLine = color;

    //    this.GetComponent<BoxCollider2D>().size = new Vector2(_texture2D.width / 100, _texture2D.height / 100);

    //    GetComponent<SpriteRenderer>().sprite = Sprite.Create(_texture2D, new Rect(0.0f, 0.0f, _texture2D.width, _texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
    //    _sprite = GetComponent<SpriteRenderer>().sprite;

    //    _widthLine = DrawerModel.WidthLine;

    //}

    public void AddCompanent(Texture2D texture2D, Color color, Sprite sprite)
    {
        _memoryTextere = texture2D;
        _texture2D = texture2D;
        _colorLine = color;
        _sprite = sprite;
        _widthLine = DrawerModel.WidthLine;
    }

    public Texture2D ApplyTexture()
    {
        return _texture2D;
    }

    public Texture2D CancelTexture()
    {
        return _memoryTextere;
    }

    // Конвертация мировых координат в координаты изображения
    protected Vector2 WorldToPixelCoordinates(Vector2 world_position)
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

    // Смена цветов в пикселях
    protected void MarkPixelToChange(int x, int y, Color color)
    {
        int array_pos = y * (int) _sprite.rect.width + x;

        if (array_pos > _curColors.Length || array_pos < 0)
            return;

        _curColors[array_pos] = color;
    }

    protected void ApplyMarkedPixelChanges()
    {
        _texture2D.SetPixels32(_curColors);
        _texture2D.Apply();
    }
}
