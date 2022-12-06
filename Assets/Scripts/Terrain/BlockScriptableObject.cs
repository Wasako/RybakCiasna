using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BlockScriptableObject")]
public class BlockScriptableObject : ScriptableObject
{
    public BreakableTile tile;
    public ItemScriptableObject itemDrop;
}
