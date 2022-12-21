using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    // objects from which game data is taken
    private GameController gameController;
    
    // HUD elements
    private TextMeshProUGUI o2ValueField;
    private Button returnButton;

    private void Awake() {
        returnButton = gameObject.transform.Find("Return Button").GetComponent<Button>();
        returnButton.onClick.AddListener(OnReturnButtonClicked);
    }

    private void Start() {
        // assign neccessary stuff
        o2ValueField = gameObject.transform.Find("O2Value").GetComponent<TextMeshProUGUI>();
        gameController = GameObject.FindObjectOfType<GameController>();

        StartCoroutine(UpdateO2());
    }

    private void OnEnable() {
        StartCoroutine(UpdateO2());
    }

    private void OnDisable() {
        StopCoroutine(UpdateO2());
    }

    public IEnumerator UpdateO2()
    {
        // buffer to load starting o2 value
        yield return new WaitForSeconds(0.01f);

        // every 1s, display current o2 value
        while (true) {

            if(gameController.GetO2() == 0)
            {
                o2ValueField.text = "";
            }

            o2ValueField.text =  gameController.GetO2().ToString(".##");
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnReturnButtonClicked()
    {
        gameController.ChangeState(GameController.GameState.Shop);
    }

}
