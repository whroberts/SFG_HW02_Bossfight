using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBase : MonoBehaviour
{
    [SerializeField] Transform _barrel;
    [SerializeField] int _damageValue = 1;

    [SerializeField] GameObject _rocket;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        ProjectileImpact(collisionObject);
    }

    protected virtual void ShootProjectile(GameObject projectile)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject newProjectile = (GameObject) Instantiate(projectile, _barrel, true);
        }
    }

    protected virtual void ShotEffects()
    {

    }

    protected virtual void ProjectileImpact(GameObject collisionObject)
    {
       
    }

    protected virtual void ImpactEffects()
    {

    }
}
