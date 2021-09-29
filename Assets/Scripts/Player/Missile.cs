using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase
{
    protected override void ShootProjectile(GameObject missile)
    {
        LaunchFeedback();
    
        Rigidbody rb;
        rb = missile.GetComponent<Rigidbody>();
        rb.velocity *= _travelSpeed;

        Destroy(gameObject, 8f);
    }
}
