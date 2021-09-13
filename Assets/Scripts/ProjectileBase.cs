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

            ImpactFeedback();
        }
    }

    

    private void Update()
    {
        if (_tc._loaded)
        {
            ShootProjectile(_tc.newProjectile);
            _tc._loaded = false;
        }
    }

    protected void LaunchFeedback()
    {
        AudioHelper.PlayClip2D(_launchSound, 1f, 0f);
        _launchEffect.Play();
    }
    

    private void ImpactFeedback()
    {
        AudioHelper.PlayClip2D(_onHitSound, 1f, 0f);
        _onHitEffect.Play();

        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponentInChildren<Collider>();

        mesh.enabled = false;
        col.enabled = false;
        Destroy(_tc.newProjectile, 3f);
    }

    

}
