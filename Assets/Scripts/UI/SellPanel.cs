using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPanel : MonoBehaviour
{
    public Inventory inventory;
    public GameObject Slot;
    public GameObject inventoryObject;
    // Start is called before the first frame update
    private void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory>();

        for(int i =0; i == inventory.getItemCount(); i++)
        {
            Instantiate(Slot, transform.position, Quaternion.identity);
        }
    }
}
