using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyParameters : MonoBehaviour
{
    [SerializeField] private int hp;

    public void TakeDamage(int dmg)
    {
        hp = hp - dmg;
        StartCoroutine(Flash());
        if (hp <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private IEnumerator Flash()
    {
        Color defaultColor = gameObject.GetComponentInParent<SpriteRenderer>().color;
        gameObject.GetComponentInParent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponentInParent<SpriteRenderer>().color = defaultColor;
    }
}
