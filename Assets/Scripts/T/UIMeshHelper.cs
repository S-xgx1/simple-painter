#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public static class UIMeshHelper
{
    public static Vector2 CalculateBottomLeftCorner(RectTransform rectTransform)
    {
        var bottomLeft = Vector2.zero;

        bottomLeft.x -= rectTransform.pivot.x;
        bottomLeft.y -= rectTransform.pivot.y;

        bottomLeft.x *= rectTransform.rect.width;
        bottomLeft.y *= rectTransform.rect.height;

        return bottomLeft;
    }

    public static Vector2 CalculateTopRightCorner(RectTransform rectTransform)
    {
        var topRight = Vector2.one;

        topRight.x -= rectTransform.pivot.x;
        topRight.y -= rectTransform.pivot.y;

        topRight.x *= rectTransform.rect.width;
        topRight.y *= rectTransform.rect.height;

        return topRight;
    }

    public static (Vector2, Vector2) CalculateCorners(RectTransform rectTransform)
    {
        var bottomLeft = Vector2.zero;
        var topRight   = Vector2.one;

        bottomLeft.x -= rectTransform.pivot.x;
        bottomLeft.y -= rectTransform.pivot.y;
        topRight.x   -= rectTransform.pivot.x;
        topRight.y   -= rectTransform.pivot.y;

        bottomLeft.x *= rectTransform.rect.width;
        bottomLeft.y *= rectTransform.rect.height;
        topRight.x   *= rectTransform.rect.width;
        topRight.y   *= rectTransform.rect.height;

        return (bottomLeft, topRight);
    }

    public static void DrawLine(VertexHelper vh, Vector2 from, Vector2 to, float size, Color color)
    {
        var length = to - from;
        if (Mathf.Approximately(length.magnitude, 0)) return;
        var o       = vh.currentVertCount;
        var breadth = new Vector2(-length.y, length.x).normalized * size;

        var vert = UIVertex.simpleVert;
        vert.position = from - 0.5f * breadth;
        vert.color    = color;
        vh.AddVert(vert);

        vert.position = from + 0.5f * breadth;
        vert.color    = color;
        vh.AddVert(vert);

        vert.position = to + 0.5f * breadth;
        vert.color    = color;
        vh.AddVert(vert);

        vert.position = to - 0.5f * breadth;
        vert.color    = color;
        vh.AddVert(vert);

        vh.AddTriangle(o, o + 1, o + 2);
        vh.AddTriangle(o    + 2, o + 3, o);
    }

    public static void DrawRect(VertexHelper vh, Vector2 bottomLeft, Vector2 topRight, Color cbl, Color ctl, Color cbr,
                                Color        ctr)
    {
        var o = vh.currentVertCount;

        var vert = UIVertex.simpleVert;

        vert.position = new Vector2(bottomLeft.x, bottomLeft.y);
        vert.color    = cbl;
        vh.AddVert(vert);

        vert.position = new Vector2(bottomLeft.x, topRight.y);
        vert.color    = ctl;
        vh.AddVert(vert);

        vert.position = new Vector2(topRight.x, topRight.y);
        vert.color    = ctr;
        vh.AddVert(vert);

        vert.position = new Vector2(topRight.x, bottomLeft.y);
        vert.color    = cbr;
        vh.AddVert(vert);

        vh.AddTriangle(o, o + 1, o + 2);
        vh.AddTriangle(o    + 2, o + 3, o);
    }

    public static void DrawRect(VertexHelper vh, Vector2 bottomLeft, Vector2 topRight, Color color) =>
        DrawRect(vh, bottomLeft, topRight, color, color, color, color);

    public static void DrawImage(VertexHelper vh, Vector2 bottomLeft, Vector2 topRight)
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
}