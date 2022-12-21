using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDiveButton : MonoBehaviour
{
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }
    
    private void OnButtonClicked()
    {
        GameObject.FindObjectOfType<GameController>().ChangeState(GameController.GameState.Underwater);
    }
}
