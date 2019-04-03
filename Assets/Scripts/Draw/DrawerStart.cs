using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerStart : MonoBehaviour
{
    public Color PenColour = Color.black;
    public int PenWidth = 30;

    public delegate void Brush_Function(Vector2 world_position);
    // This is the function called when a left click happens   Эта функция вызывается при нажитии левого клика
    // Pass in your own custom one to change the brush type    Передает ваши значения для изменения типа кисти
    // Set the default function in the Awake method            Получает стандартные настройки в методе Awake
    public Brush_Function CurrentBrush;

    public LayerMask DrawingLayers;

    public bool Reset_Canvas_On_Play = true;

    public Color ResetColour = new Color(0, 0, 0, 0);  // By default, reset the canvas to be transparent По умолчанию делает холст прозрачным

    public DrawerStart Draw;

    private Sprite _drawerSprite;
    private Texture2D _drawerTexture;

    private Vector2 _previousDragPosition;     // передыдущая позиция
    private Color[] _cleanColoursArray;        // массив чистых цветов
    //private Color transparent;                  // прозрачность
    private Color32[] _curColors;

    private bool mouse_was_previously_held_down = false;
    private bool no_drawing_on_current_drag = false;

    //////////////////////////////////////////////////////////////////////////////
    // BRUSH TYPES. Implement your own here     Типы кистей. Релизовать самостоятельно

    public void BrushTemplate(Vector2 worldPosition) //шаблон кисти
    {
        // 1. Change world position to pixel coordinates    Конвектирует мировые координаты в пиксели
        Vector2 pixelPos = WorldToPixelCoordinates(worldPosition);

        // 2. Make sure our variable for pixel array is updated in  this frame   Убедитесь что ваша переменная обновлена в массиве пикселей в этом кадре
        _curColors = _drawerTexture.GetPixels32();

        ////////////////////////////////////////////////////////////////
        // FILL IN CODE BELOW HERE  Заполнить код снизу

        // Do we care about the user left clicking and dragging?    Мы заботимся о пользовательском щелчке левой кнопкой мыши и перетаскивании?
        // If you don't, simply set the below if statement to be:   если вы не сделаете этого:
        // if (true)                                                просто установите true
        if (_previousDragPosition == Vector2.zero)
        {
            // THIS IS THE FIRST CLICK                      Это первый клик
            // FILL IN WHATEVER YOU WANT TO DO HERE         Заполните все что вы хотите здесь сделать
            // Maybe mark multiple pixels to colour?        Может пометить несколько пикселей для цвета?
            MarkPixelsToColour(pixelPos, PenWidth, PenColour);
        }
        else
        {
            // THE USER IS DRAGGING     Перетаскивание пользователем
            // Should we do stuff between the previous mouse position and the current one? Что мы будем делать при перетаскивании предыдущей позицией мыши и текущей
            ColourBetween(_previousDragPosition, pixelPos, PenWidth, PenColour);
        }
        ////////////////////////////////////////////////////////////////

        // 3. Actually apply the changes we marked earlier  Принимает изменения выполненные ранее
        // Done here to be more efficient  Используется здесь для большей эффективности
        ApplyMarkedPixelChanges();

        // 4. If dragging, update where we were previously  Если было перемещение точка обновляется до последней позиции
        _previousDragPosition = pixelPos;
    }


    // Default brush type. Has width and colour.        Тип кисти по умолчанию. Имеют шириной и цвет.
    // Pass in a point in WORLD coordinates             Передает точке мировые координаты
    // Changes the surrounding pixels of the world_point to the PenColour   Преобразует соседние пиксели с координатами world_point в цвета PenColour 
    public void PenBrush(Vector2 world_point)
    {
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

        _curColors = _drawerTexture.GetPixels32();

        if (_previousDragPosition == Vector2.zero)
        {
            // If this is the first time we've ever dragged on this image, simply colour the pixels at our mouse position
            // Если мы впервые пермещаем (во время рисования курсор) на этом изображении, просто идет закрашивание пикселей вокруг позиции мышки 
            MarkPixelsToColour(pixel_pos, PenWidth, PenColour);
        }
        else
        {
            // Colour in a line from where we were on the last update call  Закрашивание линии от между последней и предпоследней точкой
            ColourBetween(_previousDragPosition, pixel_pos, PenWidth, PenColour);
        }

        ApplyMarkedPixelChanges();

        //Debug.Log("Dimensions: " + pixelWidth + "," + pixelHeight + ". Units to pixels: " + unitsToPixels + ". Pixel pos: " + pixel_pos);
        _previousDragPosition = pixel_pos;
    }

    // Helper method used by UI to set what brush the user wants  Вспомогательный метод исобльуется UI-ем для получения кисти которую хочет пользовтель.
    // Create a new one for any new brushes you implement         создавайте новые кисти которые вы хотите использовать
    public void SetPenBrush()
    {
        // PenBrush is the NAME of the method we want to set as our current brush   PenBrush - название метода, который мы хотим установить в качестве текущей кисти
        CurrentBrush = PenBrush;
    }
    //////////////////////////////////////////////////////////////////////////////


    // This is where the magic happens.         Здесь происходит волщебство
    // Detects when user is left clicking, which then call the appropriate function    Определеят когда пользовватель нажмет ЛКМ, которая затем вызовет нужные функции
    void Update()
    {
        // Is the user holding down the left mouse button?  пользовтель удерживает ЛКМ?
        bool mouse_held_down = Input.GetMouseButton(0);
        if (mouse_held_down && !no_drawing_on_current_drag)
        {
            // Convert mouse coordinates to world coordinates   Конвектирует координаты мыши в мировые координаты
            Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the current mouse position overlaps our image   Проверяет, изменилось ли положение мыши на изображении.
            Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, DrawingLayers.value);
            if (hit != null && hit.transform != null)
            {
                // We're over the texture we're drawing on!            Мы находимся над текстурой которую должны закрасить
                // Use whatever function the current brush is          Использоваие любой функции текущей кисти
                CurrentBrush(mouse_world_position);
            }

            else
            {
                // We're not over our destination texture               мы находимся не над нашей текстурой
                _previousDragPosition = Vector2.zero;
                if (!mouse_was_previously_held_down)
                {
                    // This is a new drag where the user is left clicking off the canvas    Это новое перемещение где пользовтель кликает ЛКМ вне холста
                    // Ensure no drawing happens until a new drag is started                Убеждается что рисование не выполняется до новой начальной точки
                    no_drawing_on_current_drag = true;
                }
            }
        }
        // Mouse is released        реализация мыши
        else if (!mouse_held_down)
        {
            _previousDragPosition = Vector2.zero;
            no_drawing_on_current_drag = false;
        }
        mouse_was_previously_held_down = mouse_held_down;
    }

    // Set the colour of pixels in a straight line from start_point all the way to end_point, to ensure everything inbetween is coloured
    // Устанавливаем цвет пикселей по прямой линии от start_point до end_point, чтобы все цвета между ними были закрашены
    public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
    {
        // Get the distance from start to finish  Получает расстояение от начала до конца
        float distance = Vector2.Distance(start_point, end_point);
        Vector2 direction = (start_point - end_point).normalized;

        Vector2 cur_position = start_point;

        // Calculate how many times we should interpolate between start_point and end_point based on the amount of time that has passed since the last update
        // Подсчитайте, сколько времние мы должны потратить на интрополяцию между start_point и end_point, основываясь на количестве времени прошедшем с момента последнего обновления
        float lerp_steps = 1 / distance;

        for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
        {
            cur_position = Vector2.Lerp(start_point, end_point, lerp);
            MarkPixelsToColour(cur_position, width, color);
        }
    }


    public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        // Figure out how many pixels we need to colour in each direction (x and y) Определяет сколько пикселей нам нужно раскрасить в каждом направлении (x и y)
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;
        int extra_radius = Mathf.Min(0, pen_thickness - 2);

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {

            // Check if the X wraps around the image, so we don't draw pixels on the other side of the image  Проверяет не выходит ли Х за границы изображения, чтобы не рисовать с обратной стороны.
            if (x >= (int)_drawerSprite.rect.width
                || x < 0)
                continue;

            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                MarkPixelToChange(x, y, color_of_pen);
            }
        }
    }


    public void MarkPixelToChange(int x, int y, Color color)
    {
        // Need to transform x and y coordinates to flat coordinates of array   Нужно преобразовать координаты х и у в координаты массива
        int array_pos = y * (int)_drawerSprite.rect.width + x;

        // Check if this is a valid position    Проверка позиции
        if (array_pos > _curColors.Length || array_pos < 0)
            return;

        _curColors[array_pos] = color;
    }
    public void ApplyMarkedPixelChanges()
    {
        _drawerTexture.SetPixels32(_curColors);
        _drawerTexture.Apply();
    }


    // Directly colours pixels. This method is slower than using VerticalLineMarkPixelsToColor then using ApplyMarkedPixelChanges     Непосредвенно окрашивает пиксели. Этот метод медленее чем VerticalLineMarkPixelsToColor где используется ApplyMarkedPixelChanges
    // SetPixels32 is far faster than SetPixel              SetPixels32 быстрее чем SetPixel
    // Colours both the center pixel, and a number of pixels around the center pixel based on pen_thickness (pen radius)   Окрашивание как самого пикселя, и пикселей вокруг него
    public void ColourPixels(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        // Figure out how many pixels we need to colour in each direction (x and y)
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;
        int extra_radius = Mathf.Min(0, pen_thickness - 2);

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {
            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                _drawerTexture.SetPixel(x, y, color_of_pen);
            }
        }

        _drawerTexture.Apply();
    }


    public Vector2 WorldToPixelCoordinates(Vector2 world_position)
    {
        // Change coordinates to local coordinates of this image    конвертирует мировые координаты к локальным координатам картинки
        Vector3 local_pos = transform.InverseTransformPoint(world_position);

        // Change these to coordinates of pixels        конвектирует координаты на пиксели
        float pixelWidth = _drawerSprite.rect.width;
        float pixelHeight = _drawerSprite.rect.height;
        float unitsToPixels = pixelWidth / _drawerSprite.bounds.size.x * transform.localScale.x;

        // Need to center our coordinates   нужно центрировать наши координаты
        float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
        float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

        // Round current mouse position to nearest pixel    округдение текущего положения мыши до ближайшего пикселя
        Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

        return pixel_pos;
    }


    // Changes every pixel to be the reset colour  Изменяет цвет каждого пикселя по дефолту
    public void ResetCanvas()
    {
        _drawerTexture.SetPixels(_cleanColoursArray);
        _drawerTexture.Apply();
    }



    private void Awake()
    {
        Draw = this;
        // DEFAULT BRUSH SET HERE  Стандартная настройка кисти берется здесь.
        CurrentBrush = PenBrush;

        _drawerSprite = this.GetComponent<SpriteRenderer>().sprite;
        _drawerTexture = _drawerSprite.texture;

        // Initialize clean pixels to use Инициализация очистки пикселей
        _cleanColoursArray = new Color[(int)_drawerSprite.rect.width * (int)_drawerSprite.rect.height];
        for (int x = 0; x < _cleanColoursArray.Length; x++)
            _cleanColoursArray[x] = ResetColour;

        // Should we reset our canvas image when we hit play in the editor?  Хотите ли вы очитить поле перед запуском
        if (Reset_Canvas_On_Play)
            ResetCanvas();
    }
}
