using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWhite : MonoBehaviour
{
    public void Fill(ref Texture2D tex)
    {
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                Color color = Color.white;
                tex.SetPixel(x, y, color);
            }
        }
        tex.Apply();
    }
}
