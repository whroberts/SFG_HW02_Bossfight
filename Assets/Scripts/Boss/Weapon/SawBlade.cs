using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : BossWeaponBase
{
    bool _contact = false;
    ParticleSystem impactEffect;
    AudioSource impactAudio;

    protected override void Attack(GameObject weapon)
    {
        if (!_contact)
        {
            weapon.transform.position = _bc._sawBladeArm.position;
            weapon.transform.LookAt(_bc._player.transform);
            _rb.velocity = transform.forward * _launchSpeed;
        }
    }

    protected override void RotateEvent()
    {
        if (!_contact)
        {
            _rb.transform.Rotate(Vector3.right * _rotationStep * _launchSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        Walls walls = collision.gameObject.GetComponent<Walls>();

        if (damageable != null)
        {
            _contact = true;
            BladeImpactEffect();
            damageable.TakeDamage(_damage);
        }

        if (walls != null)
        {
            _contact = true;
            Destroy(gameObject, 1f);
        }
    }

    private void Update()
    {
        if (_contact)
        {
            OnStop();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            BladeLeaveImpact();
            damageable.TakeDamage(_damage);
        }
    }

    void BladeImpactEffect()
    {
        if (_impactEffect != null)
        {
            impactEffect = Instantiate(_impactEffect,transform.position, Quaternion.identity);

            impactAudio = AudioHelper.PlayClip2D(_impactAudio, "Impact Sound2: " + gameObject.name, 0.01f, _impactAudio.length);
        }
    }

    void BladeLeaveImpact()
    {
        if (_impactEffect != null)
        {
            Destroy(impactAudio.gameObject, 2f);
        }
    }

    void OnStop()
    {
        if (_rb.velocity.magnitude <= new Vector3(0.1f, 0.1f, 0.1f).magnitude)
        {
            if (impactAudio != null)
            {
                Destroy(impactAudio.gameObject);
            }

            if (impactEffect != null)
            {
                Destroy(impactEffect.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
