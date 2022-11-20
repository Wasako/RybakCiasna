using UnityEngine;
using UnityEngine.UI;


public class ParameterButton : MonoBehaviour
{
    [SerializeField] private ParametersScriptableObject _parametersScriptableObj;
    [SerializeField] private Parameter.Types _parameterEnum;
    private Parameter _playerParameter;
    private Button _button;

    private void Awake()
    {
        _playerParameter = _parametersScriptableObj.Parameters[(int)_parameterEnum];
        _playerParameter.Reset();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() => _playerParameter.Upgrade();
}
