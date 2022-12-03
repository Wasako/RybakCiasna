using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainHandler : MonoBehaviour
{

    private Tilemap thisTilemap;
    private Dictionary<TileBase, ItemScriptableObject> dropTable = new();
    

    private void Start() {
        // this tilemap component
        thisTilemap = gameObject.GetComponent<Tilemap>();

        // error handling: if this script is attached to an object without a tilemap
        if (!thisTilemap)
        {
            Debug.LogWarning("No tilemap found on this gameObject");
        }

        // load the SO that contains info about blocks
        BlockTableScriptableObject tempDropTable = Resources.Load("Terrain/TerrainBlocksTable") as BlockTableScriptableObject;

        // populate the drop table
        foreach (BlockScriptableObject block in tempDropTable.blockTable)
        {
            dropTable.Add(block.tile, block.itemDrop);
            // Debug.Log(block.tile + " " + block.itemDrop);
        }
        
        // unload to save memory or sth idk
        Resources.UnloadAsset(tempDropTable);
    }

    // arguments: collision with a tilemap collider
    // returns: true if a tile has been broken, false if no tilemap or no tile at position
    public bool BreakTile(Collision2D collision)
    {
        Vector3Int _gridPosition;

        TryFindTileFromCollision(collision, out bool isFound, out _gridPosition);
        
        if (!isFound) {
            return false;
        }

        AddToInventory(thisTilemap.GetTile(_gridPosition));

        // to set the tile in the tilemap to null - no sprite & collision
        thisTilemap.SetTile(_gridPosition, null);
        return true;

    }

    private void TryFindTileFromCollision(Collision2D collision, out bool isFound, out Vector3Int returnPosition)
    {
        isFound = false;
        returnPosition = Vector3Int.zero;

        // the grid on which this tilemap exists
        Grid _grid = thisTilemap.layoutGrid;

        // WorldToCell method of the Grid takes the point of contact between colliders, and
        // returns the position of the corresponding tile in a grid
        Vector3Int _gridPosition = _grid.WorldToCell(collision.GetContact(0).point);

        // Debug.Log("tile "+ _gridPosition);

        // if the tile at the collision point is null (happens very often because 
        // fucking collision points are not inside a tile sometimes)
        if (thisTilemap.GetTile(_gridPosition) == null)
        {
            // offset the collision point in the direction of the tile
            ContactPoint2D tempCon = collision.GetContact(0);
            Vector3 tempOffset = new Vector3( tempCon.point.x - tempCon.normal.x, tempCon.point.y - tempCon.normal.y);
            _gridPosition = _grid.WorldToCell( tempOffset);
        }

        // check again, if still null assume unsuccessful
        if (thisTilemap.GetTile(_gridPosition) == null)
        {
            isFound = false;
            returnPosition = Vector3Int.zero;
        }
        else
        {
            isFound = true;
            returnPosition = _gridPosition;
        }
    }

    private void AddToInventory(TileBase brokenTile)
    {
        if (dropTable[brokenTile] != null)
        {
            FindObjectOfType<Inventory>().TryAddItem(dropTable[brokenTile]);
            FindObjectOfType<Inventory>().PrintInventory();
        }
    }

}
