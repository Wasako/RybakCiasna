using UnityEngine;
using TMPro;


public class ParameterDisplay : MonoBehaviour
{
    [SerializeField] private ParametersScriptableObject _parametersScriptableObj;
    [SerializeField] private Parameter.Types _parameterEnum;
    private TextMeshProUGUI _parameterDisplay;
    private Parameter _playerParameter;

    private void Start()
    {
        _playerParameter = _parametersScriptableObj.Parameters[(int)_parameterEnum];
        _parameterDisplay = GetComponent<TextMeshProUGUI>();

        // Subscribe to ParameterUpdated
        _playerParameter.ParameterUpdated += DisplayUpdate;

        // Initial Update. Must be called after Parameter.Reset()
        DisplayUpdate();
    }

    private void DisplayUpdate() => _parameterDisplay.text = _playerParameter.Value.ToString("N2");

    private void DisplayUpdate(object sender, System.EventArgs e) => DisplayUpdate();
}
