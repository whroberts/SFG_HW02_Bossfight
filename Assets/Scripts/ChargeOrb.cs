using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeOrb : ProjectileBase
{
    protected override void ShootProjectile(GameObject orb)
    {
        Debug.Log("Shoot Orb");

        Rigidbody rb;
        rb = orb.GetComponent<Rigidbody>();
        rb.velocity *= _travelSpeed;
    }
}
