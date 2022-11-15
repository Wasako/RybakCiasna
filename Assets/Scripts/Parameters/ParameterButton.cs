using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ParameterButton : MonoBehaviour
{
    [SerializeField] private ParametersScriptableObject _playerParameters;
    [SerializeField] private Parameter.Types _parameterEnum;
    [SerializeField] private TextMeshProUGUI _parameterDisplay;
    private Parameter _playerParameter;
    private Button _button;

    private void Awake()
    {
        _playerParameter = _playerParameters.Parameters[(int)_parameterEnum];
        _playerParameter.Reset();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonCallback);

        UpdateDisplay();
    }

    private void UpdateDisplay() => _parameterDisplay.text = _playerParameter.Value.ToString("N2");

    private void ButtonCallback()
    {
        _playerParameter.Upgrade();
        UpdateDisplay();
    }
}
