using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    // fields that are relevant to config of generation 
    [Header("Tilemap options")]
    [SerializeField] Tilemap terrainTilemap;
    [SerializeField] Tile filled;

    [Header("Size of generated map")]
    [Range(0, 1000)][SerializeField] int width = 1;
    [Range(0, 1000)][SerializeField] int height = 1;

    [Header("Generation options")]
    [Tooltip("small - big chunks")][SerializeField] float smoothness = 20f;
    [Tooltip("small - more filled")][SerializeField] int fillBorder = 50;

    float seed; // should not be visible or changeable

    void Start()
    {
        // generate terrain, if none is generated yet
        if (!terrainTilemap.ContainsTile(filled)) {
            seed = Random.Range(-1000000, 1000000);
            GenerateTerrain();
        }
    }

    private void Update()
    {
        // keys to quickly generate, clear terrain
        if (Input.GetKeyDown(KeyCode.I))
        {
            ButtonNewTerrain();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ButtonSameTerrain();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ButtonClearTerrain();
        }
    }

    private void OnDrawGizmos() {
        // displays a gizmo of the size and position of terrain that is/ will be generated
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(width/2, height/-2, 0), new Vector3(width, height, 0));
    }
    
    public void GenerateTerrain()
    {
        // iterates through all tiles in defined area
        for (int x = 0; x < width; x++)
        {
            for (int y = height*-1; y < 0; y++)
            {
                // calls the perlin noise function, and if output is larger than some threshold,
                // fills in the tile
                if (Perlin(x, y) > fillBorder)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), filled);
                }
            }
        }
    }

    // function that calculates an output from Perlin noise 
    int Perlin(int xCoord, int yCoord) {
        
        float x = (float)xCoord / width * smoothness + seed;
        float y = (float)yCoord / height * smoothness + seed;

        int result = Mathf.RoundToInt (Mathf.PerlinNoise(x, y) * 100);
        return result;
    }

    // functions that are called when buttons are pressed
    // both on keyboard (I, O, P) and in inspector
    public void ButtonClearTerrain()
    {
        terrainTilemap.ClearAllTiles();
    }

    public void ButtonNewTerrain()
    {
        terrainTilemap.ClearAllTiles();
        seed = Random.Range(-1000000, 1000000);
        GenerateTerrain();
    }

    public void ButtonSameTerrain()
    {
        terrainTilemap.ClearAllTiles();
        GenerateTerrain();
    }
}
