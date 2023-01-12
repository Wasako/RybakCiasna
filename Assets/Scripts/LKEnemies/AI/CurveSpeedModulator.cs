using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionAndSpeed))]
public class CurveSpeedModulator : MonoBehaviour, ISpeedModulator
{
    private DirectionAndSpeed velocity;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float timeSpeed = 0;

    private float time = 0;

    void ISpeedModulator.Reset()
    {
        time = 0;
    }

    void Start() 
    {
        velocity = GetComponent<DirectionAndSpeed>();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        velocity.Speed = curve.Evaluate(time * timeSpeed);
    }
}
