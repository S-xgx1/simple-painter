#region

using UnityEngine;

#endregion

public class AddColorButton : MonoBehaviour
{
    public PixelCanvas  pixelCanvas;
    public ColorPreview colorPreview;
    public PaletteView  paletteView;

    public void AddColor()
    {
        pixelCanvas.selectedIdx = pixelCanvas.AddColor(colorPreview.color);
        paletteView.SetVerticesDirty();
    }
}