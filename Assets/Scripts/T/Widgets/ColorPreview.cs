#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

public class ColorPreview : Graphic
{
    public UnityEvent<Color> onColorChange;

    public override Color color
    {
        get => base.color;
        set
        {
            base.color = value;
            onColorChange?.Invoke(base.color);
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);

        vh.Clear();

        UIMeshHelper.DrawRect(vh, bl, tr, color);
    }

    public void SetR(float r)
    {
        color = new(r, color.g, color.b, color.a);
    }

    public void SetG(float g)
    {
        color = new(color.r, g, color.b, color.a);
    }

    public void SetB(float b)
    {
        color = new(color.r, color.g, b, color.a);
    }

    public void SetA(float a)
    {
        color = new(color.r, color.g, color.b, a);
    }
}