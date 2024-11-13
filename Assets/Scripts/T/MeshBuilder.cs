#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class MeshBuilder
{
    [Flags]
    public enum Face
    {
        None       = 0,
        Left       = 1,
        Right      = 2,
        XDimension = Left | Right,
        Bottom     = 4,
        Top        = 8,
        YDimension = Bottom | Top,
        Front      = 16,
        Back       = 32,
        ZDimension = Front | Back,
    }

    readonly List<Color> colors = new();

    readonly MeshTopology topology;
    readonly List<int>    triangles = new();

    readonly List<Vector3> vertices = new();

    public MeshBuilder() => topology = MeshTopology.Triangles;

    public MeshBuilder(MeshTopology topology) => this.topology = topology;

    public MeshBuilder Line(Vector3 a, Vector3 b)              => Line(a, b, Color.black);
    public MeshBuilder Line(Vector3 a, Vector3 b, Color color) => Line(a, b, color, color);

    public MeshBuilder Line(Vector3 a, Vector3 b, Color ca, Color cb)
    {
        var start = vertices.Count;
        vertices.Add(a);
        vertices.Add(b);

        colors.Add(ca);
        colors.Add(cb);

        triangles.Add(start);
        triangles.Add(start + 1);

        return this;
    }

    // a b c with CCW
    public MeshBuilder Triangle(Vector3 a, Vector3 b, Vector3 c, Color ca, Color cb, Color cc)
    {
        var start = vertices.Count;
        vertices.Add(a);
        vertices.Add(b);
        vertices.Add(c);

        colors.Add(ca);
        colors.Add(cb);
        colors.Add(cc);

        triangles.Add(start);
        triangles.Add(start + 1);
        triangles.Add(start + 2);

        return this;
    }

    public MeshBuilder Triangle(Vector3 a, Vector3 b, Vector3 c, Color color) => Triangle(a, b, c, color, color, color);

    public MeshBuilder Quad(Vector3 o, Vector3 ex, Vector3 ey, Color color)
    {
        Triangle(o, o + ey, o + ex,      color);
        Triangle(o    + ey, o + ex + ey, o + ex, color);
        return this;
    }

    public MeshBuilder Cuboid(Vector3 o, float length, float breadth, float height, Color color, Face cullFaces)
    {
        var i = Vector3.right   * length;
        var j = Vector3.forward * breadth;
        var k = Vector3.up      * height;

        if (!cullFaces.HasFlag(Face.Right)) Quad(o + i, j, k, color);

        if (!cullFaces.HasFlag(Face.Left)) Quad(o + j, -j, k, color);

        if (!cullFaces.HasFlag(Face.Front)) Quad(o, i, k, color);

        if (!cullFaces.HasFlag(Face.Back)) Quad(o + i + j, -i, k, color);

        if (!cullFaces.HasFlag(Face.Top)) Quad(o + k, i, j, color);

        if (!cullFaces.HasFlag(Face.Bottom)) Quad(o + j, i, -j, color);

        return this;
    }

    public MeshBuilder Cube(Vector3 o, float size, Color color, Face cullFaces) =>
        Cuboid(o, size, size, size, color, cullFaces);

    public MeshBuilder Circle(Vector3 o, Vector3 axis, Vector3 radius, int triangleCount, Color color)
    {
        var step = 360.0f / triangleCount;

        for (var i = 0; i < triangleCount; i++)
        {
            var next = Quaternion.AngleAxis(step, axis) * radius;
            Triangle(o, radius, next, color);
            radius = next;
        }

        return this;
    }

    public MeshBuilder Scale(float factor)
    {
        for (var i = 0; i < vertices.Count; i++) vertices[i] *= factor;

        return this;
    }

    public MeshBuilder ScaleXYZ(float factorX, float factorY, float factorZ)
    {
        for (var i = 0; i < vertices.Count; i++)
            vertices[i] = new(vertices[i].x * factorX, vertices[i].y * factorY, vertices[i].z * factorZ);
        return this;
    }

    public MeshBuilder Translate(Vector3 translation)
    {
        for (var i = 0; i < vertices.Count; i++) vertices[i] += translation;

        return this;
    }

    public Mesh Build()
    {
        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.colors   = colors.ToArray();
        mesh.SetIndices(triangles.ToArray(), topology, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}