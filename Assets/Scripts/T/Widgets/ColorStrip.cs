#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class ColorStrip : Graphic
{
    public List<Color> colors;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);

        vh.Clear();

        var step = (tr.x - bl.x) / (colors.Count - 1);
        for (var i = 0; i < colors.Count - 1; i++)
            UIMeshHelper.DrawRect(vh, new(bl.x        + i * step, bl.y), new(bl.x + (i + 1) * step, tr.y), colors[i],
                                  colors[i], colors[i + 1], colors[i              + 1]);
    }
}