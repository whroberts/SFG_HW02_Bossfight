using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BossWeaponBase : MonoBehaviour
{
    protected abstract void Attack(GameObject weapon);

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
    }

    private void FixedUpdate()
    {
        RotateEvent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            Impact();
            playerHealth.TakeDamage(_damage);
        }
        else
        {
            Impact();
        }
    }
    
    protected virtual void RotateEvent()
    {
        //Debug.Log("Rotate Event Function");
    }

    protected virtual void Impact()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider col = GetComponent<Collider>();

        if (mesh != null && col != null)
        {
            mesh.enabled = false;
            col.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
        _rb.freezeRotation = true;

        if (_impactEffect != null)
        {
            ParticleSystem impactEffect = Instantiate(_impactEffect);
            impactEffect.gameObject.transform.position = gameObject.transform.position;
            Destroy(impactEffect, 0.5f);

            AudioSource impactAudio = AudioHelper.PlayClip2D(_impactAudio, "Impact Sound: " + gameObject.name, 0.1f, _impactAudio.length);
            Destroy(impactAudio, _impactAudio.length);
        }

        Destroy(gameObject, 3f);
    }
}
