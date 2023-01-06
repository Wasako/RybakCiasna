using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UpAndDownEnemyAI : BaseEnemyAi
{
    [SerializeField]
    [Range(0f, 10f)]
    [Tooltip("How far will the AI travel verticaly")]
    protected float travelDistance;
    
    private Vector3 startingPosition;

    new void Start()
    {
        startingPosition = transform.position;
    }


    protected override void Behavior()
    {
        float step = movementSpeed * Time.deltaTime;

        if (startingPosition.x == 0f)
        {

        }
        transform.position = startingPosition + transform.up * Mathf.Sin(Time.time) * travelDistance;
    }
}
