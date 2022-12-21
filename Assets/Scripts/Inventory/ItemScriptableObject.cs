using UnityEngine;


[CreateAssetMenu(fileName = "ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    public string InventoryName;
    public int Value;
    public Sprite Sprite;
    public string Info;
}
