using UnityEngine;


[CreateAssetMenu(fileName = "ParametersScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    public string InventoryName;
    public int Value;
    public Sprite Sprite;
}
