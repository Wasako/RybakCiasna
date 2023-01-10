﻿using System.Collections;
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
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    protected override void Behavior()
    {
        float step = movementSpeed * Time.deltaTime;

        if (startingPosition.x == 0f)
        {

        }
        
        rb.MovePosition(startingPosition + transform.up * Mathf.Sin(Time.time) * travelDistance);
    }
}
