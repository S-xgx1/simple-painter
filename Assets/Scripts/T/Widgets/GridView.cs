#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class GridView : Graphic
{
    public PixelCanvas pixelCanvas;

    [Space]
    public float lineSize;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);
        vh.Clear();
        var xStep = rectTransform.rect.width  / pixelCanvas.Width;
        var yStep = rectTransform.rect.height / pixelCanvas.Height;
        for (var i = 0; i <= pixelCanvas.Height; i++)
            UIMeshHelper.DrawLine(vh, new(bl.x, bl.y + i * yStep), new(tr.x, bl.y + i * yStep), lineSize, color);

        for (var i = 0; i <= pixelCanvas.Width; i++)
            UIMeshHelper.DrawLine(vh, new(bl.x + i * xStep, bl.y), new(bl.x + i * xStep, tr.y), lineSize, color);
    }
}