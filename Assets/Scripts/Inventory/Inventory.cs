using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public event System.EventHandler MoneyUpdated;
    public static Inventory Instance;

    [SerializeField] private ParametersScriptableObject _parametersScriptableObj;
    private Parameter _inventorySpaceParameter;
    private Dictionary<ItemScriptableObject, int> _itemsCount = new();
    public int money { get; private set; } = 50;

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
        if (ItemsCount + 1 > _inventorySpaceParameter.Value) return false; // Inventory full

        // Increase item count or set it to one
        _itemsCount[item] = _itemsCount.TryGetValue(item, out int count) ? (count + 1) : 1;
        return true;
    }

    public void DropItem(ItemScriptableObject item)
    {
        _itemsCount[item] = _itemsCount.TryGetValue(item, out int count) ? Mathf.Max(count - 1, 0) : 0;
    }

    private int ItemsCount => _itemsCount.Sum(x => x.Value);

    private void Awake()
    {
        // Ensure there is only one instance of Inventory
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _inventorySpaceParameter = _parametersScriptableObj.Parameters[(int)Parameter.Types.InventorySpace];
    }

    // Converts all items into money based on their value
    private void ConvertToMoney()
    {
        foreach (var item in _itemsCount)
        {
            // money += itemValue * itemCount
            money += item.Key.Value * item.Value;
        }
        _itemsCount.Clear();

        MoneyUpdated?.Invoke(this, System.EventArgs.Empty);
    }
}
