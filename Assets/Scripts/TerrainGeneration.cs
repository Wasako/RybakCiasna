using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] int width = 1, height = 1, fillBorder = 50;
    [SerializeField] Tilemap terrainTilemap;
    [SerializeField] Tile filled, empty;

    [Range(0,100)]
    [SerializeField] float smoothness = 20f;
    float seed;

    int[][] generationResult;

    void Start()
    {
        seed = Random.Range(-1000000, 1000000);
        Generation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            seed = Random.Range(-1000000, 1000000);
            Generation();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Generation();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            terrainTilemap.ClearAllTiles();
        }
    }
    
    void Generation()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)//This will help spawn a tile on the y axis
            {
                if (Perlin(x, y) > fillBorder)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), filled);
                }
            }

        }
    }

    int Perlin(int xCoord, int yCoord) {
        
        float x = (float)xCoord / width * smoothness + seed;
        float y = (float)yCoord / width * smoothness + seed;

        int result = Mathf.RoundToInt (Mathf.PerlinNoise(x, y) * 100);
        // Debug.Log(result);
        return result;
    }

    //todo: rename parameters, add comments

}
