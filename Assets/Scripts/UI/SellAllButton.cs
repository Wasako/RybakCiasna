using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellAllButton : MonoBehaviour
{
    private Inventory inventory;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    private void Start() {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }
    
    private void OnButtonClicked()
    {
        inventory.SellAll();
    }
}
