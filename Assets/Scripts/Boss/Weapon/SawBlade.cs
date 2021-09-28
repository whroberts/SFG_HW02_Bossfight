using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : BossWeaponBase
{
    bool _contact = false;

    protected override void Attack(GameObject weapon)
    {
        weapon.transform.position = _bc._sawBladeArm.position;
        weapon.transform.LookAt(_bc._player.transform);
        _rb.velocity = transform.forward * _launchSpeed;
    }

    protected override void RotateEvent()
    {
        //_rb.transform.Rotate(Vector3.right * _rotationStep * _launchSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            ImpactEffect();
            damageable.TakeDamage(_damage);
        }
    }
}
