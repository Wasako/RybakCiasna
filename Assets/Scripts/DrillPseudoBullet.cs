using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPseudoBullet : Attack
{
    [SerializeField] private float harpoonRange = 0.2f;

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
            Destroy(gameObject);
            other.gameObject.GetComponent<TerrainHandler>().DamageTile(other, 1);
            
        }
    }
}
