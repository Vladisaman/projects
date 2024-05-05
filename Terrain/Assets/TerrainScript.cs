using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public int width;
    public AnimationCurve curve;
    public float noiseScale;
    public float heightScale;
    private Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GetTerrain(terrain.terrainData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TerrainData GetTerrain(TerrainData terrain)
    {
        terrain.heightmapResolution = width + 1;
        terrain.size = new Vector3(width, width, width);
        terrain.SetHeights(0, 0, GetMatrix());
        return terrain;
    }

    public float[,] GetMatrix()
    {
        float[,] matrix = new float[width, width];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                matrix[x, y] = curve.Evaluate(Mathf.PerlinNoise((x) / noiseScale, (y) / noiseScale) * heightScale);
            }
        }
        // + Random.Range(0, 10)
        return matrix;
    }

}