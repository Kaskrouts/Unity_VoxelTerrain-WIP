using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public List<Vector3> newVertices = new List<Vector3>();
    public List <int> newTriangles = new List<int>();
    public List<Vector3> newCollider = new List<Vector3>();
    public List<Vector2> ColorVertices = new List<Vector2>();

    public List<Vector3> HoleBlocks = new List<Vector3>();

    private Mesh mesh;
    public Gradient gradient;

    private Material newMat;

    private World world;
    public GameObject water;

    public GameObject worldStart;

    private Color[] color;

    private float maxTerrainHeight;
    private float minTerrainHeight;

    private int counter = 0;

    private int chunkSize;

    public int chunk_X;
    public int chunk_Y;
    public int chunk_Z;
    
    // 'set_chunck_size' and 'set_chunk_coordinates' are respectively responsable 
    // for locating the chunck in the 3D environment and setting it's size to the desired value

    public void setChunkCoordinates (int x ,int y ,int z)
    {
        this.chunk_X = x;
        this.chunk_Y = y;
        this.chunk_Z = z ;
    }

    public void setChunksize (int chunksize)
    {
        this.chunkSize = chunksize;
    }
    
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

    // each block with the tag '0' is considered as air
    // to generate blocks efficiently, the following 6 methods will check
    // if the above,below,right,left,front or back blocks are air (depending on the position of the block)
    // in the Top function, if the above block is affectevily onsidered as air then the top texture of thecube will
    // be generated
    void Top(int x , int y,int z)
    {
        if (Block(x,y+1,z) == 0)

        {
            newVertices.Add(new Vector3(x, y, z));
            newVertices.Add(new Vector3(x + 1, y, z));
            newVertices.Add(new Vector3(x + 1, y, z + 1));
            newVertices.Add(new Vector3(x, y, z + 1));

            generateTexture();
        }
        
    }
    void Bottom(int x , int y, int z)
    {
        if (Block(x, y - 1, z) == 0)
        {
            newVertices.Add(new Vector3(x, y - 1, z + 1));
            newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
            newVertices.Add(new Vector3(x + 1, y - 1, z));
            newVertices.Add(new Vector3(x, y - 1, z));

            generateTexture();
        }
    }

    void Right(int x , int y ,int z)
    {
        if (Block(x + 1, y, z) == 0)
        {
            newVertices.Add(new Vector3(x + 1, y - 1, z));
            newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
            newVertices.Add(new Vector3(x + 1, y, z + 1));
            newVertices.Add(new Vector3(x + 1, y, z));

            generateTexture();
        }
    }
    void Left(int x , int y ,int z)
    {
        if (Block(x - 1, y, z) == 0)
        {
            newVertices.Add(new Vector3(x, y - 1, z + 1));
            newVertices.Add(new Vector3(x, y - 1, z));
            newVertices.Add(new Vector3(x, y, z));
            newVertices.Add(new Vector3(x, y, z + 1));

            generateTexture();
        }
    }

    void Front(int x, int y, int z)
    {
        if (Block(x, y, z - 1) == 0)
        {
            newVertices.Add(new Vector3(x, y - 1, z));
            newVertices.Add(new Vector3(x + 1, y - 1, z));
            newVertices.Add(new Vector3(x + 1, y, z));
            newVertices.Add(new Vector3(x, y, z));

            generateTexture();
        }
    }

    void Back(int x, int y, int z)
    {
        if (Block(x, y, z + 1) == 0)
        {
            newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
            newVertices.Add(new Vector3(x, y - 1, z + 1));
            newVertices.Add(new Vector3(x, y, z + 1));
            newVertices.Add(new Vector3(x + 1, y, z + 1));
        
            generateTexture();
        }
    }


    // generating cubes depending on there number 
    // example each block with the tag '3' will be generated as water material
    // the same goes for grass
    // TO-DO :: impliment the other types of blocks 
    public void makeCube(int x , int y , int z )
    {

        if (Block(x, y, z) == 2)
        {
            GetComponent<Renderer>().material = Resources.Load("terrainMaterial", typeof(Material)) as Material; 
            Top(x, y, z);
            Bottom(x, y, z);
            Front(x, y, z);
            Back(x, y, z);
            Right(x, y, z);
            Left(x, y, z);
        }

        if (Block(x,y,z) == 3)
        {
            GetComponent<Renderer>().material = Resources.Load("waterMaterial", typeof(Material)) as Material;
            Top(x, y, z);
            Bottom(x, y, z);
            Front(x, y, z);
            Back(x, y, z);
            Right(x, y, z);
            Left(x, y, z);
        }

        if (Block (x,y,z) == 4)
        {
            GetComponent<Renderer>().material = Resources.Load("stoneMaterial", typeof(Material)) as Material;
            Top(x, y, z);
            Bottom(x, y, z);
            Front(x, y, z);
            Back(x, y, z);
            Right(x, y, z);
            Left(x, y, z);
        }

        if (Block(x, y, z) == 5)
        {
            GetComponent<Renderer>().material= Resources.Load("GrassMaterial", typeof(Material)) as Material;
            Top(x, y, z);
            Bottom(x, y, z);
            Front(x, y, z);
            Back(x, y, z);
            Right(x, y, z);
            Left(x, y, z);
        }
    }

    //WIP : this is supposed to check if a block is above the surface or not

    int BlockAboveSurface(int x, int y, int z)
    {
        while (Block(x, y, z) == 0)
        {
            y--;
        }

        while (Block(x, y, z) == 1)
        {
            y++;
        }

        return y;
    }

    //WIP : this function is supposed to find the blocks below sea level in a given chunk
    // and arrange the found block in 'HoleBlocks' array
    void identifyHole(int Chunkx, int Chunkz, int surface)
    {
        for (int i = Chunkx; i< chunkSize; i++)
        {
            for (int k = Chunkz; k<chunkSize; k++)
            {
                int CurrentY = BlockAboveSurface(i, 0, k);

                if (CurrentY < surface)
                {
                    HoleBlocks.Add(new Vector3(i, CurrentY, k));
                }
            }
        }
    }

    // creating a chunck using a set of cubes and identifying the max height and min height in that chunck
    // this will be helpful later to set a color gradient that varies depending on the height of the map

    public void makeChunk(int size)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    makeCube(x, y, z);
                    setMinAndMax((float)y);
                }
            }
        }

        updateMesh();
    }

    // WIP : the gradient setup 
    void colorGradient()
    {
        color = new Color[newVertices.ToArray().Length];

        for (int i = 0; i < color.Length; i++)
        {
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, newVertices[i].y);
            color[i] = gradient.Evaluate(height);
        }
        
    }

    void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();

        counter = 0;

    }


    void setMinAndMax(float y)
    {
        if (y > maxTerrainHeight)
        {
            maxTerrainHeight = y;
        }

        if (y < minTerrainHeight)
        {
            minTerrainHeight = y;
        }
    }

    void Start()
    {

        mesh = GetComponent<MeshFilter>().mesh;
        world = worldStart.GetComponent("World") as World;
        makeChunk(chunkSize);

        colorGradient();
        mesh.colors = color;

    }

    // useful to give the exact coordinates of a block in the entire world
    
    int Block(int x , int y ,int z)
    {
        return world.Block(x+chunk_X, y+chunk_Y, z+chunk_Z);
    }

}

