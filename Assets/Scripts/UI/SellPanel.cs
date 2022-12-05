using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPanel : MonoBehaviour
{
    public Inventory inventory;
    public GameObject Slot;
    public GameObject inventoryObject;
    // Start is called before the first frame update
    void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory>();
        for(int i =0; i == inventory._itemsCount.Count; i++)
        {
            Instantiate(Slot, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
