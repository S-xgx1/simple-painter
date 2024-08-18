#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class ColorPicker : MonoBehaviour
{
    public enum ColorChannel
    {
        R,
        G,
        B,
        A,
    }

    public ColorChannel lookUpChannel;

    public ColorStrip colorStrip;
    public Slider     slider;

    public void SetStrip(Color color)
    {
        colorStrip.colors.Clear();
        switch (lookUpChannel)
        {
            case ColorChannel.R:
                slider.SetValueWithoutNotify(color.r);
                color.r = 0;
                colorStrip.colors.Add(color);
                color.r = 1;
                colorStrip.colors.Add(color);
                break;

            case ColorChannel.G:
                slider.SetValueWithoutNotify(color.g);
                color.g = 0;
                colorStrip.colors.Add(color);
                color.g = 1;
                colorStrip.colors.Add(color);
                break;

            case ColorChannel.B:
                slider.SetValueWithoutNotify(color.b);
                color.b = 0;
                colorStrip.colors.Add(color);
                color.b = 1;
                colorStrip.colors.Add(color);
                break;

            case ColorChannel.A:
                slider.SetValueWithoutNotify(color.a);
                color.a = 0;
                colorStrip.colors.Add(color);
                color.a = 1;
                colorStrip.colors.Add(color);
                break;
        }

        colorStrip.SetVerticesDirty();
    }
}