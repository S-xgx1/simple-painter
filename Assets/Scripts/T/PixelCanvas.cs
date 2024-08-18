#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class PixelCanvas : MonoBehaviour
{
    public List<Color> palette = new();
    public int[,]      img;

    public int Width { get; private set; }

    public int Height { get; private set; }

    public int   selectedIdx   { get; set; }
    public Color selectedColor => palette[selectedIdx];

    public void InitCanvas(int width, int height)
    {
        selectedIdx = 0;
        img         = new int[width, height];
        this.Width  = width;
        this.Height = height;
    }

    public int AddColor(Color color)
    {
        var idx = palette.Count;
        palette.Add(color);
        return idx;
    }

    public void SetPixel(int x, int y)
    {
        SetPixel(selectedIdx, x, y);
    }

    void ClearPixel()
    {
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
            img[x, y] = 0;
    }

    public void SetPixel(int index, int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return;
        img[x, y] = index;
    }

    public Texture2D Clear()
    {
        ClearPixel();
        var texture = new Texture2D(Width, Height);
        texture.filterMode = FilterMode.Point;

        for (var y = 0; y < Height; y++)
        for (var x = 0; x < Width; x++)
            texture.SetPixel(x, y, palette[img[x, y]]);

        texture.Apply();

        return texture;
    }

    public Texture2D ExportToTexture2D()
    {
        var texture = new Texture2D(Width, Height);
        texture.filterMode = FilterMode.Point;

        for (var y = 0; y < Height; y++)
        for (var x = 0; x < Width; x++)
            texture.SetPixel(x, y, palette[img[x, y]]);

        texture.Apply();

        return texture;
    }
}