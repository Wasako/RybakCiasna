using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimplestEnemyAI : BaseEnemyAi
{
    [SerializeField]private GameObject projectilePrefab;
    [SerializeField]private float projectileSpeed;
    [SerializeField] private float projectileRate;
    private float lastProjectileAttackTime;

    protected override void Behavior()
    {
        float step = movementSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPlayer.position) > safePlayerDistance 
            && detectionPlayerDistance > Vector3.Distance(transform.position, targetPlayer.position))
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, step);
        }
        else if (Vector3.Distance(transform.position, targetPlayer.position) < safePlayerDistance - 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, -step);
            shootAtPlayer(2);
        }
        else
        {
            shootAtPlayer(1);
        }
    }
    private void shootAtPlayer(int slowMultiplayer)
    {
        if (Time.time > lastProjectileAttackTime + projectileRate * slowMultiplayer)
        {
            lastProjectileAttackTime = Time.time;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce((targetPlayer.position - transform.position).normalized * projectileSpeed);
            projectile.GetComponent<Attack>().damage = gameObject.GetComponent<EnemyManager>().enemyDamageOutput;
            Destroy(projectile, 2.75f);
        }
    }
}
