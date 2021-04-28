using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereGradient : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;

    float maxHeight;
    float minHeight;

    public Gradient gradient;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Coloring();
        mesh.colors = colors;
    }


    void Coloring()
    {
        colors = new Color[vertices.Length];
        SetMaxAndMin();

        for (int i = 0; i<vertices.Length; i++)
        {
            float CurrentHeight = vertices[i].y;
            colors[i] = gradient.Evaluate(Mathf.InverseLerp(minHeight, maxHeight, CurrentHeight));
        }

    }


    void SetMaxAndMin()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y > maxHeight)
            {
                maxHeight = vertices[i].y;
            }
            if (vertices[i].y < minHeight)
            {
                minHeight = vertices[i].y;
            }
        }
    }
}
