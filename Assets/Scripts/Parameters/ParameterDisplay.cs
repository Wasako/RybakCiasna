using UnityEngine;
using TMPro;


public class ParameterDisplay : MonoBehaviour
{
    [SerializeField] private ParametersScriptableObject _parametersScriptableObj;
    [SerializeField] private Parameter.Types _parameterEnum;
    [SerializeField] private TextMeshProUGUI _valueDisplay;
    [SerializeField] private TextMeshProUGUI _levelDisplay;
    [SerializeField] private TextMeshProUGUI _costDisplay;
    private Parameter _playerParameter;

    private void Start()
    {
        _playerParameter = _parametersScriptableObj.Parameters[(int)_parameterEnum];
        _valueDisplay = GetComponent<TextMeshProUGUI>();

        // Subscribe to ParameterUpdated
        _playerParameter.ParameterUpdated += DisplayUpdate;

        // Initial Update. Must be called after Parameter.Reset()
        DisplayUpdate();
        _costDisplay.text = _playerParameter.UpgradeCost.ToString();
    }

    private void DisplayUpdate()
    {
        _valueDisplay.text = _playerParameter.Value.ToString("N2");
        _levelDisplay.text = _playerParameter.Level.ToString();
    }

    private void DisplayUpdate(object sender, System.EventArgs e) => DisplayUpdate();
}
