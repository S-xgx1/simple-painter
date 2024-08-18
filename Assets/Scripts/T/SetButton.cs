#region

using UnityEngine;

#endregion

public class SetButton : MonoBehaviour
{
    public PixelCanvas     pixelCanvas;
    public PixelCanvasView pixelCanvasView;
    public PaletteView     paletteView;
    public ColorPreview    colorPreview;

    public void SetPalette()
    {
        pixelCanvas.palette[pixelCanvas.selectedIdx] = colorPreview.color;
        paletteView.SetVerticesDirty();
        pixelCanvasView.UpdateCanvas();
    }
}