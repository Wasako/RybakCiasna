using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this script handles breaking blocks of terrain, and adding corresponding items to inventory

public class TerrainHandler : MonoBehaviour
{
    private Tilemap thisTilemap;
    private Dictionary<TileBase, ItemScriptableObject> dropTable = new();
    private Vector3Int currentPosition, previousPosition = Vector3Int.zero;
    private int currentHealth, baseHealth;
    private SpriteRenderer tileBreakSR;
    private Sprite[] tileBreakSprites = new Sprite[3];
    

    private void Start() {
        // this tilemap component
        thisTilemap = gameObject.GetComponent<Tilemap>();

        // error handling: if this script is attached to an object without a tilemap
        if (!thisTilemap)
        {
            Debug.LogWarning("No tilemap found on this gameObject");
        }

        // load the SO that contains info about blocks
        BlockTableScriptableObject tempDropTable = Resources.Load("TerrainBlocksTable") as BlockTableScriptableObject;

        // populate the drop table
        foreach (BlockScriptableObject block in tempDropTable.blockTable)
        {
            dropTable.Add(block.tile, block.itemDrop);
            // Debug.Log(block.tile + " " + block.itemDrop);
        }
        
        // unload to save memory or sth idk
        Resources.UnloadAsset(tempDropTable);

        // assign the sprite renderer for sprites for tile breaking
        tileBreakSR = gameObject.GetComponentInChildren<SpriteRenderer>();
        tileBreakSR.enabled = false;

        tileBreakSprites[0] = Resources.LoadAll("tile_breaking")[1] as Sprite;
        tileBreakSprites[1] = Resources.LoadAll("tile_breaking")[2] as Sprite;
        tileBreakSprites[2] = Resources.LoadAll("tile_breaking")[3] as Sprite;
        
    }

    // arguments: collision with a tilemap collider
    // returns: true if a tile has been broken, false if not
    public bool DamageTile(Collision2D collision, int damage)
    {

        TryFindTileFromCollision(collision, out bool isFound, out currentPosition);

        if (!isFound) {
            return false;
        }

        ApplyDamage(damage);

        if (currentHealth <= 0)
        {
            AddToInventory(thisTilemap.GetTile(currentPosition));

            // to set the tile in the tilemap to null - no sprite & collision
            thisTilemap.SetTile(currentPosition, null);
            RemoveBreakingSprite();
            return true;
        }

        DisplayBreakingSprite();

        return false;

    }

    private void ApplyDamage(int damage)
    {   
        if (currentPosition != previousPosition)
        {
            // Debug.Log("new tile");
            baseHealth = thisTilemap.GetTile<BreakableTile>(currentPosition).health;
            currentHealth = baseHealth;
        }

        // Debug.Log(currentHealth);
        previousPosition = currentPosition;
        currentHealth -= damage;
    }

    private void DisplayBreakingSprite()
    {
        // display correct sprite
        tileBreakSR.enabled = true;
        tileBreakSR.transform.position = currentPosition + thisTilemap.tileAnchor;

        float currentHealthPercent = (float) currentHealth / (float) baseHealth;
        // Debug.Log(currentHealthPercent);

        if (currentHealthPercent >= 0.65f)
        {
            tileBreakSR.sprite = tileBreakSprites[0];
        }
        else if (currentHealthPercent >=0.32f)
        {
            tileBreakSR.sprite = tileBreakSprites[1];
        }
        else
        {
            tileBreakSR.sprite = tileBreakSprites[2];
        }
    }

    private void RemoveBreakingSprite()
    {
        tileBreakSR.enabled = false;
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
