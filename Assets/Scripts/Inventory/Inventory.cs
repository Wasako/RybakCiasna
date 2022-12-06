using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public event System.EventHandler MoneyUpdated;
    public event System.EventHandler InventoryUpdated;
    public static Inventory Instance;

    [SerializeField] private ParameterScriptableObject _parameterScriptableObj;
    private Dictionary<ItemScriptableObject, int> _itemsCount = new();
    public int money { get; private set; } = 50;

    public void PrintInventory()
    {
        string _msg = "";

        foreach (KeyValuePair<ItemScriptableObject, int> kvp in _itemsCount)
        {
            _msg = _msg + kvp.Key + ": "+ kvp.Value + " ";
        }

        Debug.Log(_msg);
    }

    // Pays money if there is sufficient amount
    public bool TryBuy(int cost)
    {
        if (money < cost) return false;
        money -= cost;
        MoneyUpdated?.Invoke(this, System.EventArgs.Empty);
        return true;
    }

    // Adds item if there is empty space for it
    public bool TryAddItem(ItemScriptableObject item)
    {
        if (ItemsCount + 1 > _parameterScriptableObj.Value) return false; // Inventory full

        // Increase item count or set it to one
        _itemsCount[item] = _itemsCount.TryGetValue(item, out int count) ? (count + 1) : 1;

        InventoryUpdated?.Invoke(this, System.EventArgs.Empty);
        return true;
    }

    public void DropItem(ItemScriptableObject item)
    {
        _itemsCount[item] = _itemsCount.TryGetValue(item, out int count) ? Mathf.Max(count - 1, 0) : 0;
        InventoryUpdated?.Invoke(this, System.EventArgs.Empty);
    }

    private int ItemsCount => _itemsCount.Sum(x => x.Value);
    public int getItemCount() {return ItemsCount;}

    private void Awake()
    {
        // Ensure there is only one instance of Inventory
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Converts item into money based on its value
    private void ConvertToMoney(ItemScriptableObject item)
    {
        // money += itemValue * itemCount
        money += item.Value * (_itemsCount.TryGetValue(item, out int count) ? count : 0);
        _itemsCount.Remove(item);

        InventoryUpdated?.Invoke(this, System.EventArgs.Empty);
        MoneyUpdated?.Invoke(this, System.EventArgs.Empty);
    }
}
