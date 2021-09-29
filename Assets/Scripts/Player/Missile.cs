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

        _launchVolume = 0.001f;
        _impactVolume = 0.03f;

        Destroy(gameObject, 8f);
    }
}
