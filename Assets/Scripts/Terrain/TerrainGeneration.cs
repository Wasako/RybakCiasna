using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This script handles the generation of terrain, settings for it,
// and control of it from the inspector

public class TerrainGeneration : MonoBehaviour
{
    // fields that are relevant to config of generation 
    [Header("Tilemap options")]
    [SerializeField] Tilemap terrainTilemap;
    [SerializeField] Tile rockTile, oreTile, plantTile, goldTile;

    [Header("Size of generated map")]
    [Range(0, 1000)][SerializeField] int width = 10;
    [Range(0, 1000)][SerializeField] int height = 10;

    [Header("Generation options")]
    [Tooltip("small - big chunks")][SerializeField] float smoothness = 20f;
    [Range(0, 100)][Tooltip("small - more filled")][SerializeField] int rockBorder = 40;
    [Range(0, 100)][Tooltip("small - more filled")][SerializeField] int oreBorder = 50;
    [Range(0, 100)][Tooltip("small - more filled")][SerializeField] int goldBorder = 60;
    [Range(0, 100)][SerializeField] int plantChance = 50;

    float seed; // should not be visible or changeable

    // private Dictionary<TileBase, ItemScriptableObject> dropTable = new();

    // do przepisania!! z użyciem SO czy tam innych rzeczy


    void Start()
    {
        /*
        // load the SO that contains info about blocks
        BlockTableScriptableObject tempDropTable = Resources.Load("TerrainBlocksTable") as BlockTableScriptableObject;

        // populate the drop table
        foreach (BlockScriptableObject block in tempDropTable.blockTable)
        {
            dropTable.Add(block.tile, block.itemDrop);
            // Debug.Log(block.tile + " " + block.itemDrop);
        } 
         */
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
                // fills in the corresponding tile
                int _perlinOutput = Perlin(x,y);


                if (_perlinOutput > rockBorder && _perlinOutput <= oreBorder)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), rockTile);
                }
                else if (_perlinOutput > oreBorder && _perlinOutput <= goldBorder)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), oreTile);
                }
                else if (_perlinOutput > goldBorder)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), goldTile);
                }
                else if (terrainTilemap.GetTile(new Vector3Int(x, y-1, 0)) == rockTile && Random.Range(0, 101) < plantChance)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y-1, 0), plantTile);   
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
