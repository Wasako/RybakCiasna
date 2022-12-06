using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : Tile
{
    public int baseHealth = 1;
    private int currentHealth;

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        currentHealth = baseHealth;
        return true;
    }

    // return 1 if tile gets broken, 0 if doesnt
    public bool MineBlock(int damage)
    {
        // Debug.Log(currentHealth);

        currentHealth -= damage;

        if (currentHealth <=0)
        {
            currentHealth = 0;
            // Debug.Log("mined");
            return true;
        }
        // Debug.Log("notmined");
        return false;
    }

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
