using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : Attack
{
    [SerializeField] private float harpoonRange = 5f;

    public override void First()
    {
        throw new System.NotImplementedException();
    }

    public override void Second()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        Destroy(gameObject, harpoonRange);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.GetComponent<TerrainHandler>()) // if collision is with the terrain
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // stop all movement of a projectile
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            
            // after colliding with a block, projectiles can't collide with each other
            // layer 10 - NoCollisionHarpoon - has no collision with other Harpoons
            gameObject.layer = 10; 
        }

        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<BaseEnemyParameters>().TakeDamage(damage);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
