using UnityEngine;
using TMPro;


public class ParameterDisplay : MonoBehaviour
{
    [SerializeField] private ParameterScriptableObject _parameterScriptableObj;
    [SerializeField] private TextMeshProUGUI _valueDisplay;
    [SerializeField] private TextMeshProUGUI _levelDisplay;
    [SerializeField] private TextMeshProUGUI _costDisplay;

    private void Start()
    {
        _valueDisplay = GetComponent<TextMeshProUGUI>();

        // Subscribe to ParameterUpdated
        _parameterScriptableObj.ParameterUpdated += DisplayUpdate;

        // Initial Update. Must be called after Parameter.Reset()
        DisplayUpdate();
        _costDisplay.text = _parameterScriptableObj.UpgradeCost.ToString();
    }

    private void DisplayUpdate()
    {
        _valueDisplay.text = _parameterScriptableObj.Value.ToString("N2");
        _levelDisplay.text = _parameterScriptableObj.Level.ToString();
    }

    private void DisplayUpdate(object sender, System.EventArgs e) => DisplayUpdate();
}
