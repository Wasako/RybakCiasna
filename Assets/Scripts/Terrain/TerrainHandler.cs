using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainHandler : MonoBehaviour
{
    
    public int BreakTile(Collision2D collision)
    {
        Tilemap _tilemap = gameObject.GetComponent<Tilemap>();

        if (!_tilemap)
        {
            Debug.LogWarning("No tilemap found on this gameObject");
            return 0;
        }

        Grid _grid = _tilemap.layoutGrid;

        Vector3Int _gridPosition = _grid.WorldToCell(collision.GetContact(0).point);

        Debug.Log("tile "+ _gridPosition);
        
        _tilemap.SetTile(_gridPosition, null);
        return 1;

    }

    /*  smth like that to Harpoon.cs and instantiate the drill projectile at the tip of the drill

    private void OnCollisionEnter2D(Collision2D other) {
        if (gameObject.name.Contains("DrillPseudoBullet") && other.gameObject.GetComponent<TerrainHandler>())
        {
            other.gameObject.GetComponent<TerrainHandler>().BreakTile(other);
        }
    }
     */
}
