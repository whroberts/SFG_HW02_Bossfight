using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BossWeaponBase
{
    [Header("Individual Item")]
    [SerializeField] protected AudioClip _hitPlayerSound = null;
    [SerializeField] protected ParticleSystem _hitPlayerEffect = null;

    //bool _impact = false;

    protected override void Attack(GameObject weapon)
    {
        weapon.transform.position = _bc._launcher.position;
        weapon.transform.rotation = _bc._launcher.rotation;
        //!!!! randomizes position rather than randomizing starting location

        //weapon.transform.position = new Vector3(weapon.transform.position.x + Random.Range(-5f, 5f), 
        //weapon.transform.position.y + Random.Range(-0.5f, 1f), weapon.transform.position.z + Random.Range(-2f, 2f));
        _rb.velocity = _bc._launcher.up * _launchSpeed * Random.Range(1f, 1.25f);
    }

    public void ExtraTrigger(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        Rock rock = other.gameObject.GetComponent<Rock>();
        BossHealth boss = other.gameObject.GetComponent<BossHealth>();

        if (boss == null && rock == null)
        {
            if (damageable != null)
            {
                _impactAudio = _hitPlayerSound;
                _impactEffect = _hitPlayerEffect;
                ImpactEffect();
                damageable.TakeDamage(_damage);
            }
        }
    }

    protected override void RotateEvent()
    {
        _rb.transform.Rotate(Vector3.one * _rotationStep * _launchSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (_bc.NewRocksObject[_bc.NewRocksObject.Length-1] == this.gameObject)
        {
            _bossController._attacking = false;
        }
    }

}
