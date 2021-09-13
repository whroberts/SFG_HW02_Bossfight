using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : ProjectileBase
{
    protected override void ShootProjectile(GameObject missle)
    {
        Debug.Log("Shoot Missle");
        Rigidbody rb;
        rb = missle.GetComponent<Rigidbody>();
        rb.velocity *= _travelSpeed;
    }
}
