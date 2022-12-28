using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryEventArgs : System.EventArgs
{
    public ItemScriptableObject item;
    public int newValue;
}

public class Inventory : MonoBehaviour
{
    public event System.EventHandler MoneyUpdated;
    public event System.EventHandler<InventoryEventArgs> InventoryUpdated;
    public static Inventory Instance;

    [SerializeField] private ParameterScriptableObject _inventorySizeParameter;
    private Dictionary<ItemScriptableObject, int> _itemsCount = new();
    public int money { get; private set; } = 0;

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
        if (ItemsCount + 1 > _inventorySizeParameter.Value) return false; // Inventory full

        // Increase item count or set it to one
        _itemsCount[item] = _itemsCount.TryGetValue(item, out int count) ? (count + 1) : 1;

        var itemArgs = new InventoryEventArgs();
        itemArgs.item = item;
        itemArgs.newValue = _itemsCount[item];

        InventoryUpdated?.Invoke(this, itemArgs);
        return true;
    }

    public void DropItem(ItemScriptableObject item)
    {
        _itemsCount[item] = _itemsCount.TryGetValue(item, out int count) ? Mathf.Max(count - 1, 0) : 0;

        var itemArgs = new InventoryEventArgs();
        itemArgs.item = item;
        itemArgs.newValue = _itemsCount[item];

        InventoryUpdated?.Invoke(this, itemArgs);
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

        // set up the parameters for InventoryUpdated event
        var itemArgs = new InventoryEventArgs();
        itemArgs.item = item;
        itemArgs.newValue = 0;

        // invoke events
        InventoryUpdated?.Invoke(this, itemArgs);
        MoneyUpdated?.Invoke(this, System.EventArgs.Empty);
    }

    public void SellAll()
    {
        if (ItemsCount == 0) {return;}

        List<ItemScriptableObject> tempList = new();

        foreach (KeyValuePair<ItemScriptableObject, int> kvp in _itemsCount)
        {
            tempList.Add(kvp.Key);
        }

        foreach (ItemScriptableObject item in tempList)
        {
            ConvertToMoney(item);
        }
    }

    public bool SendCurrentInventory()
    {
        if (ItemsCount == 0) {return false;}

        List<ItemScriptableObject> tempList = new();

        foreach (KeyValuePair<ItemScriptableObject, int> kvp in _itemsCount)
        {
            var itemArgs = new InventoryEventArgs();
            itemArgs.item = kvp.Key;
            itemArgs.newValue = kvp.Value;

            InventoryUpdated?.Invoke(this, itemArgs);
        }

        return true;
    }
}
