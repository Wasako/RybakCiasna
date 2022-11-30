using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAi : MonoBehaviour
{
    [Header("AI Settings")]
    protected Transform targetPlayer;
    [SerializeField]
    [Tooltip("How fast the AI will rotate?")]
    protected float rotationSpeed;
    [SerializeField]
    [Tooltip("How fast will the AI walk?")]
    protected float movementSpeed;
    [SerializeField]
    [Range(0f, 10f)]
    [Tooltip("How far from the player will the AI stop walking and start shooting?")]
    protected float safePlayerDistance;
    [SerializeField]
    [Range(0f, 10f)]
    [Tooltip("How far from the player will the AI start walking to player")]
    protected float detectionPlayerDistance;
    [SerializeField]
    [Tooltip("The AI's Rigidbody2D component")]
    protected Rigidbody2D rb;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            targetPlayer = player.transform;
        }
    }

    void Update()
    {
        Behavior();
    }

    protected abstract void Behavior();
}
