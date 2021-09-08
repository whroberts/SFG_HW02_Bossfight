using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RocketBase : MonoBehaviour
{

    protected abstract void ShootProjectile(GameObject projectile);

    [SerializeField] Transform _barrel;
    [SerializeField] int _damageValue = 1;

    [SerializeField] ParticleSystem _launchEffect;
    [SerializeField] ParticleSystem _hitEffect;
    [SerializeField] AudioClip _launchSound;
    [SerializeField] AudioClip _hitSound;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        ProjectileImpact(collisionObject);
    }

    protected virtual void ProjectileImpact(GameObject collisionObject)
    {
       
    }

    protected virtual void ImpactEffects()
    {

    }
}
