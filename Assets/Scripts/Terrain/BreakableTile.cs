using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : Tile
{
    public int health;
    public int generationMethod; // 1 - from noise with border, 2 - by chance, 3 - plant
    public float generationValue; // chance
    // public int minDepth, maxDepth;

    /* public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        return true;
    } */

    #if UNITY_EDITOR
    [UnityEditor.MenuItem("Assets/Create/Breakable Tile")]
    public static void Create()
     {
             var name = "Default File Name";
             var path = UnityEditor.EditorUtility.SaveFilePanelInProject(
                     $"Save {name}", name, "asset", $"Save {name}", "Assets/Terrain/TileAssets");
     
         if (path == "") return;
                 UnityEditor.AssetDatabase.CreateAsset(CreateInstance<BreakableTile>(), path);
     }
     #endif
}
