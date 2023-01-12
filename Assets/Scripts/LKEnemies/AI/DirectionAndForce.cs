using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ConstantDirectionForce : HasDirection
{
    [field: SerializeField]
    public float force { get; set; }

    private Rigidbody2D rigid;

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        rigid.AddForce(direction * force, ForceMode2D.Force);
    }
}
