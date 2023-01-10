using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRightEnemyAI : BaseEnemyAi
{
    [SerializeField]
    [Range(0f, 10f)]
    [Tooltip("How far will the AI travel horizontaly")]
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

        if (startingPosition.y == 0f)
        {

        }
        rb.MovePosition(startingPosition + transform.right * Mathf.Sin(Time.time) * travelDistance);
    }
}
