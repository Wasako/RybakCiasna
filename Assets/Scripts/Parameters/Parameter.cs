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
        Newparameter,
        Total
    }
    [SerializeField] private AnimationCurve _progressionFunction = AnimationCurve.Linear(1f, 0f, 2f, 1f);
    [SerializeField] private int _maxUpgrade = _defaultValue;
    private const int _defaultValue = 1;
    private int _multiplier = _defaultValue;

    // Current parameter value
    public float Value => _progressionFunction.Evaluate(_multiplier);

    public void Upgrade()
    {
        _multiplier = Mathf.Min(_multiplier + 1, _maxUpgrade);
        ParameterUpdated.Invoke(this, System.EventArgs.Empty);
    }

    // Resets parameter to the default value
    public void Reset() => _multiplier = _defaultValue;
}
