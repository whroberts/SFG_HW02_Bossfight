using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeOrb : ProjectileBase
{
    [Header("Orb Stats")]
    [SerializeField] float _minimumChargeTime = 1.0f;

    protected override void ShootProjectile(GameObject orb)
    {
        if (_tc._timeCharged >= _minimumChargeTime)
        {
            Debug.Log("Shoot Orb");
            LaunchFeedback();

            Rigidbody rb;
            rb = orb.GetComponent<Rigidbody>();
            rb.velocity *= _travelSpeed;

            orb.transform.localScale = Vector3.one * _tc._timeCharged * 0.5f;

            StartCoroutine(WaitForDestroy());
        } 
        else
        {
            Debug.Log("Orb Not Charged");
            Destroy(gameObject);
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(8f);

        ImpactFeedback();
        Destroy(gameObject, 8f);
    }
}
