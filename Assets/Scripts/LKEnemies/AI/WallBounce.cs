using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionAndSpeed))]
public class WallBounce : MonoBehaviour
{
    private DirectionAndSpeed velocity;

    private void Start()
    {
        velocity = GetComponent<DirectionAndSpeed>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var contacts = collision.contacts;

        var normal = contacts[0].normal;

        if (velocity != null) velocity.Direction = Vector2.Reflect(velocity.Direction, normal);
    }
}

