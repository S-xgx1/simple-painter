#region

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

public class PaletteView : Graphic, IPointerDownHandler
{
    public PixelCanvas   pixelCanvas;
    public ColorPreview  colorPreview;
    public RectTransform selection;

    public int width;

    public float unitSize => rectTransform.rect.width / width;

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, eventData.position, null, out var localPoint);

        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);

        var col = (int)((localPoint.x - bl.x)         / unitSize);
        var row = (int)((tr.y         - localPoint.y) / unitSize);
        var idx = row * width + col;

        if (idx < pixelCanvas.palette.Count)
        {
            pixelCanvas.selectedIdx = idx;
            if (selection != null) selection.anchoredPosition = new(col * unitSize, row * unitSize);
            colorPreview.color = pixelCanvas.selectedColor;
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);
        if (selection != null)
        {
            selection.sizeDelta        = unitSize * Vector2.one;
            selection.anchoredPosition = unitSize * new Vector2(pixelCanvas.selectedIdx % width, pixelCanvas.selectedIdx / width);
        }

        vh.Clear();

        for (var i = 0; i < pixelCanvas.palette.Count; i++)
        {
            var col = i % width;
            var row = i / width;

            var bottomLeft = new Vector2(bl.x + unitSize * col, tr.y - unitSize * (row + 1));
            var topRight   = new Vector2(bl.x + unitSize * (col                        + 1), tr.y - unitSize * row);

            UIMeshHelper.DrawRect(vh, bottomLeft, topRight, pixelCanvas.palette[i]);
        }
    }
}