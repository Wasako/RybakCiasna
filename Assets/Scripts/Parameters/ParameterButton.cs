using UnityEngine;
using UnityEngine.UI;


public class ParameterButton : MonoBehaviour
{
    [SerializeField] private ParameterScriptableObject _parameterScriptableObj;
    private Button _button;

    private void Awake()
    {
        _parameterScriptableObj.Reset();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() => _parameterScriptableObj.Upgrade();
}
