using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class ChargeOrbNew : ProjectileBase
{
    public event Action Charging = delegate { };

    [Header("Orb Stats")]
    [SerializeField] float _minimumChargeTime = 1.0f;

    //ChargeAttackBar _chargeAttackBar;

    private float _orbSize = 0f;
    public float OrbSize => _orbSize;

    /*
    private void Awake()
    {
        _chargeAttackBar = FindObjectOfType<ChargeAttackBar>();
        _chargeAttackBar.enabled = true;
    }
    */

    protected override void ShootProjectile(GameObject orb)
    {
        while (_tc.Charging)
        {
            Rigidbody rb;
            rb = orb.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;

            orb.transform.localScale = Vector3.one * _tc.TimeCharged * 0.5f;
            _orbSize = orb.transform.localScale.x;
            Charging?.Invoke();
        }

        if (!_tc.Charging && _tc.TimeCharged >= _minimumChargeTime)
        {
            Debug.Log("Shoot Orb");
            LaunchFeedback();
            //rb.velocity *= _travelSpeed;
            
            //StartCoroutine(WaitForDestroy());
        }
        else
        {
            Debug.Log("Orb Not Charged");
            Destroy(gameObject);
        }
    }

        /*
        if (_tc.TimeCharged >= _minimumChargeTime)
        {
            Charging?.Invoke();

            Debug.Log("Shoot Orb");
            LaunchFeedback();

            Rigidbody rb;
            rb = orb.GetComponent<Rigidbody>();
            //rb.velocity = Vector3.zero;
            rb.velocity *= _travelSpeed;

            orb.transform.localScale = Vector3.one * _tc.TimeCharged * 0.5f;

            _orbSize = orb.transform.localScale.x;

            //StartCoroutine(WaitForDestroy());
        } 
        else
        {
            Debug.Log("Orb Not Charged");
            Destroy(gameObject);
        }
        */

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(8f);

        ImpactFeedback();
        Destroy(gameObject, 8f);
    }
}
