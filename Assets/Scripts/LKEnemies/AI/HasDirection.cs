using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDirection : MonoBehaviour
{
    public virtual Vector2 Direction
    {
        get => direction;
        set
        {
            direction = value;
            if (direction.magnitude > 0.01f)
            {
                direction.Normalize();
            }
            else
            {
                direction = Vector2.zero;
            }
        }
    }

    [SerializeField]
    protected Vector2 direction;

    protected virtual void Start()
    {
        Direction = direction;
    }
}
