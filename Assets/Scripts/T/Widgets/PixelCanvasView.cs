using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PixelCanvasView : Graphic, IPointerDownHandler, IDragHandler
{
    public PixelCanvas pixelCanvas;

    Texture2D                 canvasTexture;
    public override Texture   mainTexture => canvasTexture;
    public          Texture2D Texture     => canvasTexture;

    protected override void Start()
    {
        base.Start();
        pixelCanvas.InitCanvas(50, 50);
        canvasTexture = pixelCanvas.ExportToTexture2D();
    }

    public void UpdateCanvas()
    {
        canvasTexture = pixelCanvas.ExportToTexture2D();
        SetMaterialDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var (bottomLeft, topRight) = UIMeshHelper.CalculateCorners(rectTransform);

        vh.Clear();

        DrawCanvas(vh, bottomLeft, topRight);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pixelCanvas.selectedIdx = 1;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null,
                                                                out var localPoint);

        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);

        var col = (int)((localPoint.x - bl.x) * pixelCanvas.Width  / rectTransform.rect.width);
        var row = (int)((localPoint.y - bl.y) * pixelCanvas.Height / rectTransform.rect.height);

        pixelCanvas.SetPixel(col, row);

        UpdateCanvas();
    }


    void DrawCanvas(VertexHelper vh, Vector2 bottomLeft, Vector2 topRight)
    {
        var o = vh.currentVertCount;

        var vert = UIVertex.simpleVert;

        vert.position = new Vector2(bottomLeft.x, bottomLeft.y);
        vert.uv0      = new Vector2(0,            0);
        vh.AddVert(vert);

        vert.position = new Vector2(bottomLeft.x, topRight.y);
        vert.uv0      = new Vector2(0,            1);
        vh.AddVert(vert);

        vert.position = new Vector2(topRight.x, topRight.y);
        vert.uv0      = new Vector2(1,          1);
        vh.AddVert(vert);

        vert.position = new Vector2(topRight.x, bottomLeft.y);
        vert.uv0      = new Vector2(1,          0);
        vh.AddVert(vert);

        vh.AddTriangle(o, o + 1, o + 2);
        vh.AddTriangle(o    + 2, o + 3, o);
    }

    public void OnDrag(PointerEventData eventData)
    {
        pixelCanvas.selectedIdx = 1;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null,
                                                                out var localPoint);

        var (bl, tr) = UIMeshHelper.CalculateCorners(rectTransform);

        var col = (int)((localPoint.x - bl.x) * pixelCanvas.Width  / rectTransform.rect.width);
        var row = (int)((localPoint.y - bl.y) * pixelCanvas.Height / rectTransform.rect.height);

        pixelCanvas.SetPixel(col, row);

        UpdateCanvas();
    }

    public void Clear()
    {
        canvasTexture = pixelCanvas.Clear();
        SetMaterialDirty();
    }
}