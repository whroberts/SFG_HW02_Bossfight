using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeRocket : RocketBase
{
    [SerializeField] ParticleSystem _muzzleParticles;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _muzzleSound;
    [SerializeField] AudioClip _impactSound;

    protected override void ImpactEffects()
    {
        ProjectileExplosion();
    }
    private void ProjectileExplosion()
    {
        if (_impactParticles != null)
        {
            _impactParticles.Play();
            //_impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
        }

        if (_impactParticles != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }
}
