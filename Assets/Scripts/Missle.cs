using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : ProjectileBase
{
    [SerializeField] float _travelSpeed = 10f;

    [Header("Effects")]

    protected override void ShootProjectile(GameObject missle)
    {
        Debug.Log("Shoot Missle");
        Rigidbody rb;
        rb = missle.GetComponent<Rigidbody>();
        rb.velocity *= _travelSpeed;
    }
}
