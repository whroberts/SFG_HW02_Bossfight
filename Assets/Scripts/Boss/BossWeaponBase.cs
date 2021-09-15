using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BossWeaponBase : MonoBehaviour
{
    protected abstract void Attack(GameObject weapon);

    [Header("___")]
    [SerializeField] protected float _launchSpeed;
    [SerializeField] protected float _rotationStep;

    [Header("Effects")]
    [SerializeField] ParticleSystem _impactEffect;

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
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.name.Contains("Ground"))
        {
            Impact();
        } 
    }
    
    protected virtual void RotateEvent()
    {
        Debug.Log("Rotate Event Function");
    }

    protected virtual void Impact()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider col = GetComponent<Collider>();
        mesh.enabled = false;
        col.enabled = false;
        _rb.freezeRotation = true;

        ParticleSystem impactEffect = Instantiate(_impactEffect);
        impactEffect.gameObject.transform.position = gameObject.transform.position;
        Destroy(impactEffect, 0.5f);

        Destroy(gameObject, 3f);
    }
}
