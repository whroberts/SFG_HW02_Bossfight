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
    bool _charging = false;
    bool _launched = false;

    GameObject newOrb;
    Rigidbody _rb;

    private float _orbSize = 0f;
    public float OrbSize => _orbSize;

    protected override void ShootProjectile(GameObject orb)
    {
        _startTime = Time.time;
        newOrb = orb;
        _rb = orb.GetComponent<Rigidbody>();
        ChargeBar.enabled = true;
    }

    private void Update()
    {
        CurrentlyCharging(newOrb);
    }

    private void CurrentlyCharging(GameObject orb)
    {
        if (!_launched)
        {
            if (_tc.Charging)
            {
                _charging = true;
                _rb.velocity = Vector3.zero;

                orb.transform.localScale = Vector3.one * ChargeTime();
                _orbSize = orb.transform.localScale.x;

            }
            else if (!_tc.Charging)
            {
                _charging = false;

                if (_tc._timeCharged >= _minimumChargeTime)
                {
                    _launched = true;
                    LaunchProjectile();
                }
                else if (_tc._timeCharged < _minimumChargeTime)
                {
                    Destroy(gameObject);
                    ChargeBar.enabled = false;
                }
            }
        }

        Charging?.Invoke();
    }

    private void LaunchProjectile()
    {
        if (_launched && !_charging)
        {
            LaunchFeedback();
            _tc._timeCharged = 0f;
            _rb.velocity = transform.forward * _travelSpeed;
            ChargeBar.enabled = false;
            //StartCoroutine(WaitForDestroy());
        }
}

    private float ChargeTime()
    {
        chargeTime = Time.time - _startTime;

        return Mathf.Clamp(chargeTime, 0f, 3f);
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(8f);

        ImpactFeedback();
        Destroy(gameObject, 8f);
    }

    private void OnDestroy()
    {
        _launched = false;
    }
}
