using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RunningEnemyAI : BaseEnemyAi
{
    protected override void Behavior()
    {
        float step = movementSpeed * Time.deltaTime;

        if(Vector3.Distance(transform.position, targetPlayer.position) > safePlayerDistance && detectionPlayerDistance > Vector3.Distance(transform.position, targetPlayer.position))
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, step);
    }
}
