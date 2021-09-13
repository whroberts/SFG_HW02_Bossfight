using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected abstract void ShootProjectile(GameObject projectile);

    [Header("Stats")]
    [SerializeField] int _damageValue = 1;

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
    

    private void ImpactFeedback()
    {
        //play stuff
        Destroy(_tc.newProjectile);
    }

    

}
