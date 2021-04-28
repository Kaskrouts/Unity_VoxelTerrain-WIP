using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    Mesh mesh;

    public List<Vector3> newVertices = new List<Vector3>();
    public List<int> newTriangles = new List<int>();
    public List<Vector3> newCollider = new List<Vector3>();

    private int counter = 0;

    // useful to generate the texture of a block (cube)
    void generateTexture()
    {

        newTriangles.Add(4 * counter + 0);
        newTriangles.Add(4 * counter + 2);
        newTriangles.Add(4 * counter + 1);
        newTriangles.Add(4 * counter + 0);
        newTriangles.Add(4 * counter + 3);
        newTriangles.Add(4 * counter + 2);

        counter++;
    }

    // adds the corresponding vertices depending on the surface of the cube
    void Top(int x, int y, int z)
    {

            newVertices.Add(new Vector3(x, y, z));
            newVertices.Add(new Vector3(x + 1, y, z));
            newVertices.Add(new Vector3(x + 1, y, z + 1));
            newVertices.Add(new Vector3(x, y, z + 1));

            generateTexture();
        

    }
    void Bottom(int x, int y, int z)
    {
        
        
            newVertices.Add(new Vector3(x, y - 1, z + 1));
            newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
            newVertices.Add(new Vector3(x + 1, y - 1, z));
            newVertices.Add(new Vector3(x, y - 1, z));

            generateTexture();
        
    }

    void Right(int x, int y, int z)
    {
        
            newVertices.Add(new Vector3(x + 1, y - 1, z));
            newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
            newVertices.Add(new Vector3(x + 1, y, z + 1));
            newVertices.Add(new Vector3(x + 1, y, z));

            generateTexture();
        
    }
    void Left(int x, int y, int z)
    {
        
            newVertices.Add(new Vector3(x, y - 1, z + 1));
            newVertices.Add(new Vector3(x, y - 1, z));
            newVertices.Add(new Vector3(x, y, z));
            newVertices.Add(new Vector3(x, y, z + 1));

            generateTexture();
        
    }

    void Front(int x, int y, int z)
    {
        
            newVertices.Add(new Vector3(x, y - 1, z));
            newVertices.Add(new Vector3(x + 1, y - 1, z));
            newVertices.Add(new Vector3(x + 1, y, z));
            newVertices.Add(new Vector3(x, y, z));

            generateTexture();
        
    }

    void Back(int x, int y, int z)
    {
        
        
            newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
            newVertices.Add(new Vector3(x, y - 1, z + 1));
            newVertices.Add(new Vector3(x, y, z + 1));
            newVertices.Add(new Vector3(x + 1, y, z + 1));

            generateTexture();
        
    }

    // creates each vertices and triangle needed to generate the texture of all 6 sides of a cube

    public void makeCube(int x, int y, int z)
    {
     
            Top(x, y, z);
            Bottom(x, y, z);
            Front(x, y, z);
            Back(x, y, z);
            Right(x, y, z);
            Left(x, y, z);
        

    }

    void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();

        counter = 0;

    }


    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        updateMesh();
       // makeCube(0, 0, 0);   // test
    }

}

