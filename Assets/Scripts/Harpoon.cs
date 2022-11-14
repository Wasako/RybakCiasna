using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private float harpoonRange = 5f;
    private void Start()
    {
        Destroy(gameObject, harpoonRange);
    }
}
