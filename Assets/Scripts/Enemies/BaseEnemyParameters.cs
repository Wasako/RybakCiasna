using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyParameters : MonoBehaviour
{
    [SerializeField]
    public int hp;

    public void TakeDamage(int dmg)
    {
        hp = hp - dmg;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
