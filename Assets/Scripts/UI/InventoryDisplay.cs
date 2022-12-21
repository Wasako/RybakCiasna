using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    private Inventory inventoryObj;
    [SerializeField] private GameObject itemLayout;

    // inventory data
    private Dictionary<ItemScriptableObject, int> inventory = new();

    private void Start() {
        inventoryObj = GameObject.FindObjectOfType<Inventory>();
        inventoryObj.InventoryUpdated += UpdateInventory; // subscribe to InventoryUpdated event
    }

        private void UpdateInventory(object sender, InventoryEventArgs eventArgs)
    {
        // after receiving event, update the private dictionary with new data
        if (eventArgs.newValue == 0)
        {
            inventory.Remove(eventArgs.item);
        }
        else
        {
            inventory[eventArgs.item] = eventArgs.newValue;
        }

        // update the HUD: (this is a bad method to do this but it'll work for the showcase)
        // 1. clear all existing items in HUD
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // 2. instantiate new items in HUD
        foreach (KeyValuePair<ItemScriptableObject, int> kvp in inventory)
        {
            itemLayout.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = kvp.Key.name;
            itemLayout.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>().text = kvp.Value.ToString();
            Instantiate(itemLayout, gameObject.transform);
        }

        // PrintInventory();
    }

    private void PrintInventory()
    {
        string _msg = "Current inventory: ";
        foreach (KeyValuePair<ItemScriptableObject, int> kvp in inventory)
        {
            _msg = _msg + kvp.Key + ": "+ kvp.Value + " ";
        }    
        Debug.Log(_msg);
    }
}
