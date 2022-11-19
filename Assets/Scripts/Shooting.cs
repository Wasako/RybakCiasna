using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject harpoonPrefab;

    public float shootingPower = 20f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject harpoon = Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D harpoonRb = harpoon.GetComponent<Rigidbody2D>();
        harpoonRb.AddForce(firePoint.right * shootingPower, ForceMode2D.Impulse);
    }
}