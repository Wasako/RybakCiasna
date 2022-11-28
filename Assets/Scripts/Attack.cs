using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage;
    public string damageType;
    public float attackRate;
    public bool malee;
    public float lastAttackTime;

    public abstract void First();
    public abstract void Second();
}
