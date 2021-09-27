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
    [SerializeField] ParticleSystem _launchEffect;
    [SerializeField] ParticleSystem _impactEffect;
    [SerializeField] AudioClip _launchAudio;
    [SerializeField] AudioClip _impactAudio;

    protected BossWeaponController _bc;
    protected Rigidbody _rb;

    private void Awake()
    {
        _bc = FindObjectOfType<BossWeaponController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
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
        else
        {
            ImpactEffect();
        }
    }

    protected virtual void LaunchEffect()
    {
        ParticleSystem launchEffect = Instantiate(_launchEffect);
        launchEffect.gameObject.transform.position = gameObject.transform.position;
        Destroy(launchEffect.gameObject, 0.5f);

        AudioSource launchAudio = AudioHelper.PlayClip2D(_launchAudio, "Launch Sound: " + gameObject.name, 0.01f, _launchAudio.length);
        Destroy(launchAudio.gameObject, _launchAudio.length);
    }


    protected virtual void ImpactEffect()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider col = GetComponent<Collider>();

        if (mesh != null && col != null)
        {
            mesh.enabled = false;
            col.enabled = false;
        }
        _rb.freezeRotation = true;

        if (_impactEffect != null)
        {
            ParticleSystem impactEffect = Instantiate(_impactEffect);
            impactEffect.gameObject.transform.position = gameObject.transform.position;
            Destroy(impactEffect.gameObject, 0.5f);

            AudioSource impactAudio = AudioHelper.PlayClip2D(_impactAudio, "Impact Sound: " + gameObject.name, 0.01f, _impactAudio.length);
            Destroy(impactAudio.gameObject, 2f);
            Destroy(gameObject, 3f);
        }
    }
}
