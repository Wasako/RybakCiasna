using UnityEngine;
using TMPro;


public class ShopManager : MonoBehaviour
{
    [SerializeField] private ParametersScriptableObject _playerParameters;
    [SerializeField] private TextMeshProUGUI _speedDisplay;
    [SerializeField] private TextMeshProUGUI _damageDisplay;
    private int _speedMultiplier = 1;
    private int _damageMultiplier = 1;

    private void Awake() => UpdateParameters();

    public void UpgradeSpeed()
    {
        ++_speedMultiplier;
        UpdateParameters();
    }

    public void UpgradeDamage()
    {
        ++_damageMultiplier;
        UpdateParameters();
    }

    private void UpdateParameters()
    {
        // Update values in scriptable object
        _playerParameters._playerSpeed = _speedMultiplier * .2f;
        _playerParameters._playerDamage = _damageMultiplier * 10f;

        // Update values displayed in shop
        _speedDisplay.text = string.Format("{0:N1}", _playerParameters._playerSpeed);
        _damageDisplay.text = string.Format("{0:N1}", _playerParameters._playerDamage);
    }

    public void ShowUI(bool active)
    {

    }
}
