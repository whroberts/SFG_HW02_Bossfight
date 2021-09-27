using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class ChargeOrb : ProjectileBase
{
    public event Action Charging = delegate { };

    [Header("Orb Stats")]
    [SerializeField] float _minimumChargeTime = 1.0f;

    float _startTime = 0;
    float chargeTime = 0;

    GameObject newOrb;
    Rigidbody _rb;

    private float _orbSize = 0f;
    public float OrbSize => _orbSize;

    private void Awake()
    {
        _chargeBar.enabled = true;
    }

    protected override void ShootProjectile(GameObject orb)
    {
        _startTime = Time.time;
        newOrb = orb;
        _rb = orb.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CurrentlyCharging(newOrb);
        LaunchProjectile(newOrb);
    }

    private void CurrentlyCharging(GameObject orb)
    {
        if (_tc.Charging)
        {
            _rb.velocity = Vector3.zero;

            orb.transform.localScale = Vector3.one * ChargeTime();
            _orbSize = orb.transform.localScale.x;

            //Charging?.Invoke();
        }
    }

    private void LaunchProjectile(GameObject orb)
    {
        if (!_tc.Charging && _tc.TimeCharged >= _minimumChargeTime)
        {
            Debug.Log("Shoot Orb");
            LaunchFeedback();

            _rb.velocity = transform.forward * _travelSpeed;

            //StartCoroutine(WaitForDestroy());
        }
    }

    private float ChargeTime()
    {
        chargeTime = Time.time - _startTime;

        return chargeTime;
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(8f);

        ImpactFeedback();
        Destroy(gameObject, 8f);
    }
}
