using GameUtils.Time;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicBurst : HasDirection
{
    [field: SerializeField]
    public float force { get; set; }

    [field: SerializeField]
    public float period { get; set; }

    private Rigidbody2D rigid;

    private AutoTimeMachine burstMachine;

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody2D>();
        burstMachine = new AutoTimeMachine(DoBurst, period);
    }

    private void FixedUpdate()
    {
        burstMachine.Forward(Time.fixedDeltaTime);
    }

    private void DoBurst()
    {
        rigid.AddForce(Direction * force, ForceMode2D.Impulse);
    }

    private void OnValidate()
    {
        if (burstMachine != null)
        {
            burstMachine.Interval = period;
        }
    }
}
