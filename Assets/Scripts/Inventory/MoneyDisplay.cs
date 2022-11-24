using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyDisplay;

    private void Start()
    {
        Inventory.Instance.MoneyUpdated += DisplayUpdate;
        DisplayUpdate();
    }

    private void DisplayUpdate() => _moneyDisplay.text = Inventory.Instance.money.ToString();

    private void DisplayUpdate(object sender, System.EventArgs e) => DisplayUpdate();
}
