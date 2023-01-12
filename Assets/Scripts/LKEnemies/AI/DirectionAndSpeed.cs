using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirectionAndSpeed : HasDirection
{
    public override Vector2 Direction
    {
        set
        {
            base.Direction = value;
            UpdateVelocity();
        }
    }
    
    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            UpdateVelocity();
        }
    }

    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D rigid;

    protected override void Start()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        base.Start();
        Speed = speed;
    }

    private void UpdateVelocity()
    {
        rigid.velocity = direction * speed;
    }
}
