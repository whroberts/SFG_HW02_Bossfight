using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class ChargeOrb : ProjectileBase
{
    public event Action Charging = delegate { };

    [Header("Orb Stats")]
    [SerializeField] float _minimumChargeTime = 1.0f;
    [SerializeField] float _electricDamage = 1f;

    [Header("Individual Data")]
    [SerializeField] AudioClip _orbFailedChargeAudio = null;
    [SerializeField] ParticleSystem _orbFailedChargeEffect = null;
    [SerializeField] ParticleSystem _bossElectrified = null;

    float _startTime = 0;
    float chargeTime = 0;
    bool _charging = false;
    bool _launched = false;
    bool _failed = false;

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

                var matrix = transform.localToWorldMatrix;
                var position = new Vector3(matrix[0, 3], matrix[1, 3], matrix[2, 3]);

                Vector3 positionOffset = _tc.transform.position - position;

                transform.position += positionOffset;

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
                    _failed = true;
                    ChargeBar.enabled = false;
                }
                if (_failed)
                {
                    _launched = true;
                    OnFailedCharge();
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
            _rb.velocity = _tc.transform.forward * _travelSpeed;
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

    private void OnFailedCharge()
    {
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponent<Collider>();
        ParticleSystem lighting = GetComponentInChildren<ParticleSystem>();

        if (mesh != null && col != null)
        {
            mesh.enabled = false;
            col.enabled = false;
            lighting.Stop();
        }

        AudioSource failedChargeAudio = AudioHelper.PlayClip2D(_orbFailedChargeAudio, "Failed Charge Audio: " + gameObject.name.ToString(), 0.1f, _orbFailedChargeAudio.length);
        failedChargeAudio.gameObject.transform.position = gameObject.transform.position;
        ParticleSystem failedChargeEffect = Instantiate(_orbFailedChargeEffect, transform.position, Quaternion.identity);
        Destroy(failedChargeEffect.gameObject, 1f);
        Destroy(gameObject, 2f);
    }

    public virtual IEnumerator ApplyElectric(GameObject boss)
    {
        Rigidbody rb = boss.GetComponent<Rigidbody>();
        ParticleSystem bossElectrified = Instantiate(_bossElectrified, boss.transform, false);
        IDamageable damageable = boss.GetComponent<IDamageable>();

        TempImpact();

        for (int i = 0; i < bossElectrified.main.duration; i++)
        {
            rb.rotation = Quaternion.Euler(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(177f, 183f), UnityEngine.Random.Range(-3f, 3f));
            damageable.TakeDamage(_electricDamage * _orbSize);

            yield return new WaitForSeconds(1f);

            if (i == bossElectrified.main.duration - 2)
            {
                ImpactFeedback();
            }
            else if (i == bossElectrified.main.duration - 1)
            {
                Destroy(bossElectrified.gameObject, 1f);
            }
        }
    }

    void TempImpact()
    {
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponent<Collider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        if (mesh != null && col != null)
        {
            mesh.enabled = false;
            col.enabled = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        _failed = false;
        _launched = false;
    }
}
