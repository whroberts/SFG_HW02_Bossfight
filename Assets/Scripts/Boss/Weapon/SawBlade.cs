using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : BossWeaponBase
{
    bool _contact = false;
    ParticleSystem impactEffect;
    AudioSource impactAudio;

    protected override void Attack(GameObject sawBlade)
    {
        if (_bc.Burst)
        {
            BurstAttack(sawBlade);
        }
        else if (!_bc.Burst)
        {
            RegularAttack(sawBlade);
        }
        _launchVolume = 0.01f;
        _impactVolume = 0.05f;

    }

    private void RegularAttack(GameObject sawBlade)
    {
        if (!_contact)
        {
            sawBlade.transform.position = _bc._sawBladeArm.position;
            sawBlade.transform.LookAt(_bc._player.transform);
            _rb.velocity = _bc._sawBladeArm.transform.forward * _launchSpeed;
        }
    }

    private void BurstAttack(GameObject sawBlade)
    {
        if (!_contact)
        {

            sawBlade.transform.position = _bc._sawBladeArm.position;
            sawBlade.transform.LookAt(_bc._player.transform);
            _rb.velocity = _bc._sawBladeArm.transform.forward * _launchSpeed;
            

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
        OnStop();
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
        if (_impactEffect != null && _impactAudio != null)
        {
            impactEffect =  Instantiate(_impactEffect, gameObject.transform, false);

            impactAudio = AudioHelper.PlayClip2D(_impactAudio, "Impact Sound2: " + gameObject.name, 0.04f, _impactAudio.length, 0f);
        }
    }

    void BladeLeaveImpact()
    {
        if (_impactEffect != null)
        {
            //Destroy(impactAudio.gameObject);
        }
    }

    void OnStop()
    {
        if (_rb.velocity.magnitude <= new Vector3(0.1f, 0.1f, 0.1f).magnitude)
        {
            if (impactAudio != null && impactEffect != null)
            {
                Destroy(impactAudio.gameObject);
                Destroy(impactEffect.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_bc.NewSawBlades[_bc.NewSawBlades.Length - 1] == this.gameObject)
        {
            _bossController._attacking = false;
            _bossController._lastEvent = Time.time;
        }
    }
}
