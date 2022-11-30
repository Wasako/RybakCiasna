using UnityEngine;


[System.Serializable]
public class Parameter
{
    public event System.EventHandler ParameterUpdated;
    public enum Types
    {
        PlayerSpeed,
        PlayerDamage,
        DrillSpeed,
        O2Capacity,
        InventorySpace,
        Total
    }
    [SerializeField] private AnimationCurve _progressionFunction = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] private int _maxUpgrade = 1;
    [SerializeField] private int _upgradeCost;
    private const int _startLevel = 1;

    // Current parameter value
    public float Value => _progressionFunction.Evaluate(Level);
    public int Level { get; private set; } = _startLevel;
    public int UpgradeCost => _upgradeCost;

    public void Upgrade()
    {
        // Check if there is enough money to buy upgrade
        if (!Inventory.Instance.TryBuy(_upgradeCost)) return;

        Level = Mathf.Min(Level + 1, _maxUpgrade);
        ParameterUpdated?.Invoke(this, System.EventArgs.Empty);
    }

    // Resets parameter to the default value
    public void Reset() => Level = _startLevel;
}
