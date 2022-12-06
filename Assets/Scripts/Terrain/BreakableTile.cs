using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : Tile
{
    public int health;

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        return true;
    }

    // return 1 if tile gets broken, 0 if doesnt
    /* public bool MineBlock(int damage)
    {
        // Debug.Log(health);

        health -= damage;

        if (health <=0)
        {
            health = 0;
            // Debug.Log("mined");
            return true;
        }
        // Debug.Log("notmined");
        return false;
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
