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
}
