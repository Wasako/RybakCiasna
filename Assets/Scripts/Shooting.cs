using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject harpoonPrefab;
    public float fireRate;
    [SerializeField] private ParameterScriptableObject _fireRateParameter;
    [SerializeField] private float _o2DrainRate;

    private float nextTimeToFire = 0f;

    public float shootingPower = 20f;

    void Start() => fireRate = _fireRateParameter.Value; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

            // Drain o2 - this causes an error, because it's 0 - I think it sould have a ParameterSO instead of a float?
            // GameController.Instance.DrainO2(_o2DrainRate);
        }
    }

    void Shoot()
    {
        GameObject harpoon = Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D harpoonRb = harpoon.GetComponent<Rigidbody2D>();
        harpoonRb.AddForce(firePoint.right * shootingPower, ForceMode2D.Impulse);
    }
}
