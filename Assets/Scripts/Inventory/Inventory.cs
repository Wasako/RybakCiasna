using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event System.EventHandler MoneyUpdated;
    public static Inventory Instance;
    private int _money = 50;

    private void Awake()
    {
        // Ensure there is only one instance of Inventory
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool TryBuy(int cost)
    {
        if (_money < cost) return false;
        _money -= cost;
        MoneyUpdated.Invoke(this, System.EventArgs.Empty);
        return true;
    }
}
