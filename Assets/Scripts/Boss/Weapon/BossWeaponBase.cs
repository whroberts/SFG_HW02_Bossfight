using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BossWeaponBase : MonoBehaviour
{
    protected abstract void Attack(GameObject weapon);
    protected abstract void RotateEvent();

    [Header("Stats")]
    [SerializeField] protected int _damage;
    [SerializeField] protected float _launchSpeed;
    [SerializeField] protected float _rotationStep;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem _launchEffect = null;
    [SerializeField] protected ParticleSystem _impactEffect = null;
    [SerializeField] protected AudioClip _launchAudio = null;
    [SerializeField] protected AudioClip _impactAudio = null;

    protected BossWeaponController _bc;
    protected BossController _bossController;
    protected Rigidbody _rb;

    protected float _launchVolume = 0.05f;
    protected float _impactVolume = 0.05f;
    public float LaunchVolume => _launchVolume;
    public float ImpactVolume => _impactVolume;

    Vector3 _launchLocation;

    private void Awake()
    {
        _bc = FindObjectOfType<BossWeaponController>();
        _bossController = FindObjectOfType<BossController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        RotateEvent();
        Attack(gameObject);
        LaunchEffect();

    }

    private void FixedUpdate()
    {
        RotateEvent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            ImpactEffect();
            damageable.TakeDamage(_damage);
        }
        ImpactEffect();
    }

    protected virtual void LaunchEffect()
    {
        ParticleSystem launchEffect = Instantiate(_launchEffect, transform.position, Quaternion.identity);
        //launchEffect.gameObject.transform.position = _launchLocation;
        Destroy(launchEffect.gameObject, 0.5f);

        AudioSource launchAudio = AudioHelper.PlayClip2D(_launchAudio, "Launch Sound: " + gameObject.name.ToString(), _launchVolume, _launchAudio.length, 0f);
        launchAudio.gameObject.transform.position = gameObject.transform.position;
        Destroy(launchAudio.gameObject, _launchAudio.length);
    }


    protected virtual void ImpactEffect()
    {
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponent<Collider>();
        if (mesh != null && col != null)
        {
            mesh.enabled = false;
            col.enabled = false;
        }
        _rb.freezeRotation = true;

        if (_impactEffect != null)
        {
            ParticleSystem impactEffect = Instantiate(_impactEffect, gameObject.transform.position, Quaternion.identity);
            //impactEffect.gameObject.transform.position = gameObject.transform.position;
            Destroy(impactEffect.gameObject, 2f);

            AudioSource impactAudio = AudioHelper.PlayClip2D(_impactAudio, "Impact Sound: " + gameObject.name.ToString(), _impactVolume, _impactAudio.length, 0f);
            impactAudio.gameObject.transform.position = gameObject.transform.position;
            Destroy(impactAudio.gameObject, 2f);
            Destroy(gameObject, 2f);
        }
    }
}
