using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPseudoBullet : Harpoon
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<TerrainHandler>()) // if collision is with the terrain
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<TerrainHandler>().DamageTile(other);
            
        }
    }
}
