using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainHandler : MonoBehaviour
{
    [SerializeField] private Tile oreItemTile;
    [SerializeField] private ItemScriptableObject oreItem;

    // arguments: collision with a tilemap collider
    // returns: true if a tile has been broken, false if no tilemap or no tile at position
    public bool BreakTile(Collision2D collision)
    {
        // this tilemap component
        Tilemap _tilemap = gameObject.GetComponent<Tilemap>();

        // error handling: if this script is attached to an object without a tilemap
        if (!_tilemap)
        {
            Debug.LogWarning("No tilemap found on this gameObject");
            return false;
        }

        // the grid on which this tilemap exists
        Grid _grid = _tilemap.layoutGrid;

        // WorldToCell method of the Grid takes the point of contact between colliders, and
        // returns the position of the corresponding tile in a grid
        Vector3Int _gridPosition = _grid.WorldToCell(collision.GetContact(0).point);

        Debug.Log("tile "+ _gridPosition);

        // if the tile at the collision point is null (happens very often because fucking collision points are always not inside a tile)
        if (_tilemap.GetTile(_gridPosition) == null)
        {
            // offset the collision point in the direction of the tile
            ContactPoint2D tempCon = collision.GetContact(0);
            Vector3 tempOffset = new Vector3( tempCon.point.x - tempCon.normal.x, tempCon.point.y - tempCon.normal.y);
            _gridPosition = _grid.WorldToCell( tempOffset);

            // if did not work, assume breaking the tile was unsuccessful
            if (_tilemap.GetTile(_gridPosition) == null)
            {
                return false;
            }
        }

        //chujowy sposób na robienie tego, naprawię
        if(_tilemap.GetTile(_gridPosition) == oreItemTile)
        {
            FindObjectOfType<Inventory>().TryAddItem(oreItem);
            FindObjectOfType<Inventory>().PrintInventory();
        }
        
        // to set the tile in the tilemap to null - no sprite & collision
        _tilemap.SetTile(_gridPosition, null);
        return true;

    }

}
