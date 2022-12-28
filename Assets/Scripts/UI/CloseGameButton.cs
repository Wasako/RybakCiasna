using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseGameButton : MonoBehaviour
{
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }
    
    private void OnButtonClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        Application.Quit();

        #endif
    }
}
