using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BlockScriptableObject")]
public class BlockScriptableObject : ScriptableObject
{
    public Tile tile;
    public int hardness;
    public ItemScriptableObject itemDrop;
    public int generationMethod; // 1 - from noise with border, 2 - by chance, 3 - plant
    public float generationValue; // border or chance
}
