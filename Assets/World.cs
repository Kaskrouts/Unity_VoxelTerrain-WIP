using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public List<Vector3> HoleBlocks = new List<Vector3>();

    public int World_x = 16;
    public int World_y = 16;
    public int World_z = 16;

    public float minHeight;
    public float maxHeight;

    private Color[] color;

    Gradient gradient;

    public GameObject water;

    public GameObject Chunk;
    public GameObject[,,] Chunks;
    public int Chunksize;

    public int[,,] WorldData;

    // generate world using Perlin noise (a 2D random Grayscale map)
    // the topography of the surface is based on the mapping of each pixel in the 
    // perlin noise 2d map, depending on the grayscale of that pixel, to the 3D world
    
    void Start()
    {
        WorldData = new int[World_x, World_y, World_z];

        for (int x = 0; x < World_x; x++)
        {
            for (int z = 0; z < World_y; z++)
            {
                int surface = PerlinNoise(x, 50, z, 70, 4, 2f) + 64;
                int holes = PerlinNoise(x, 20, z, 70, 1f, -1f);

                for (int y = 0; y < World_y; y++)
                {
                    if (surface - holes < minHeight) { minHeight = surface - holes; }
                    if (surface - holes > maxHeight) { maxHeight = surface - holes; }
                    
                    //basically if y is below the sea-level (in this case surface - holes), generate water blocks
                    // otherwise put grass 
                    if (y < surface - holes)
                    {
                        WorldData[x, y, z] = 2;
                    }

                    if (y < surface - holes && y > surface - holes -2)
                    {
                        WorldData[x, y, z] = 5;
                    }
                }

            }
        }
        initWater();
        createChunks();
    }

    // instantiates chuncks based on the dimention of the world map
    // ex : if the world is 128 x 128 then we need 4 chuncks each 64 x 64
    // in a square formation
    void createChunks()
    {
        Chunks = new GameObject[Mathf.FloorToInt(World_x / Chunksize),
            Mathf.FloorToInt(World_y / Chunksize),
            Mathf.FloorToInt(World_z / Chunksize)];

        for (int x = 0; x < Chunks.GetLength(0); x++)
        {
            for (int y = 0; y < Chunks.GetLength(1); y++)
            {
                for (int z = 0; z < Chunks.GetLength(2); z++)
                {
                    Chunks[x, y, z] = Instantiate(Chunk, new Vector3(x * Chunksize, y * Chunksize,
                        z * Chunksize), new Quaternion(0, 0, 0, 0));
                    Chunk chunkSkript = Chunks[x, y, z].GetComponent("Chunk") as Chunk;

                    chunkSkript.setChunksize(Chunksize);

                    chunkSkript.worldStart = gameObject;
                    chunkSkript.setChunkCoordinates(Chunksize * x, Chunksize * y, Chunksize * z);

                }
            }
        }

    }

    // this states that if the blocks are in the given bounds of the world, return its data
    // otherwise return 1
    public int Block(int x, int y, int z)
    {
        if (x >= World_x || x < 0 || y >= World_y || y < 0 || z >= World_z || z < 0)
            return 1;
        return WorldData[x, y, z];
    }

    // strait up stolen from the Perlin noise library

    int PerlinNoise(int x, int y, int z, float scale, float height, float power)
    {
        float rValue;
        rValue = Noises.Noise.GetNoise(((double)x / scale), ((double)y / scale), ((double)z) / scale);
        rValue *= height;

        if (power != 0)
        {
            rValue = Mathf.Pow(rValue, power);
        }

        return (int)rValue;
    }

    // filling the holes in the world with water
    // TO DO :: this should not always be the case, the world can also have some holes here and there
    // will be later implemented, when i get myself some free time
    public void initWater()
    {
        for (int x = 0; x < World_x; x++)
        {
            for (int z = 0; z < World_y; z++)
            {
                for (int y = 0; y < 64; y++)
                {
                     //(WorldData[x, y, z] != 2 && WorldData[x, y, z] == 0)
                        WorldData[x, y, z] = 3;
                }
            }
        }
    }
}
 