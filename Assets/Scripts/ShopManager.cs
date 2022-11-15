using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public enum ParameterEnum
    {
        PLAYER_SPEED,
        PLAYER_DAMAGE,
        DRILL_SPEED,
        O2_CAPACITY,
        TOTAL
    }

    [SerializeField] private ParametersScriptableObject _playerParameters;
    [SerializeField] private TextMeshProUGUI _speedDisplay;
    [SerializeField] private TextMeshProUGUI _damageDisplay;
    private int[] _parameterMultipliers = new int[(int)ParameterEnum.TOTAL];

    private void Awake() => UpdateScriptableParameters();

    public void UpdateParameter(ParameterEnumHolder parameterHolder)
    {
        switch (parameterHolder.Parameter)
        {
            case ParameterEnum.PLAYER_SPEED:

        }
    }
    private void UpdateScriptableParameters()
    {
        // Update values in scriptable object
        _playerParameters._playerSpeed = _speedMultiplier * .2f;
        _playerParameters._playerDamage = _damageMultiplier * 10f;

        // Update values displayed in shop
        _speedDisplay.text = string.Format("{0:N1}", _playerParameters._playerSpeed);
        _damageDisplay.text = string.Format("{0:N1}", _playerParameters._playerDamage);
    }
}
