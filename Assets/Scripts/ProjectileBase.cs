using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected abstract void ShootProjectile(GameObject projectile);

    [Header("Stats")]
    [SerializeField] int _damageValue = 1;
    [SerializeField] protected float _travelSpeed = 10f;

    [Header("Standard Effects")]
    [SerializeField] ParticleSystem _launchEffect;
    [SerializeField] ParticleSystem _onHitEffect;
    [SerializeField] AudioClip _launchSound;
    [SerializeField] AudioClip _onHitSound;

    TurretController _tc;

    

    private void Awake()
    {
        _tc = FindObjectOfType<TurretController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player == null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage(_damageValue);
            }
            //ImpactFeedback();
        }
    }

    

    private void Update()
    {
        if (_tc._loaded)
        {
            ShootProjectile(_tc.newProjectile);
            //LaunchFeedback();
            _tc._loaded = false;
        }
    }

    protected void LaunchFeedback()
    {
        AudioHelper.PlayClip2D(_launchSound, .05f, 1.5f);
        print("play");

        ParticleSystem newSystem = Instantiate(_launchEffect, _tc.transform, false);
        newSystem.Play();
    }
    

    private void ImpactFeedback()
    {
        AudioHelper.PlayClip2D(_onHitSound, .1f, 2.5f);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponentInChildren<Collider>();

        ParticleSystem newSystem = Instantiate(_onHitEffect, _tc.newProjectile.transform, false);
        newSystem.Play();

        mesh.enabled = false;
        col.enabled = false;
        Destroy(_tc.newProjectile, 3f);
    }

    

}
