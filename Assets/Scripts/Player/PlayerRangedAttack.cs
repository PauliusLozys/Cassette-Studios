using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot() //shooting logic
    {
        Debug.Log("Ranged attack");

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
