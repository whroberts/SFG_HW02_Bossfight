using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected abstract void ShootProjectile(GameObject projectile);

    [Header("Stats")]
    [SerializeField] protected float _damageValue = 1;
    [SerializeField] protected float _travelSpeed = 10f;

    [Header("Standard Effects")]
    [SerializeField] ParticleSystem _launchEffect;
    [SerializeField] ParticleSystem _onHitEffect;
    [SerializeField] AudioClip _launchSound;
    [SerializeField] AudioClip _onHitSound;

    protected TurretController _tc;

    private void Awake()
    {
        _tc = FindObjectOfType<TurretController>();
    }

    private void Start()
    {
        ShootProjectile(_tc.newProjectile);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player == null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage(_damageValue);
            }
            ImpactFeedback();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Damage(_damageValue);
            Debug.Log(_damageValue);
            ImpactFeedback();
        }
    }

    protected void LaunchFeedback()
    {
        AudioHelper.PlayClip2D(_launchSound, "Launch Feedback: " + _tc.newProjectile.name, .05f, _launchSound.length);

        ParticleSystem launchParticleEffect = Instantiate(_launchEffect, _tc.transform, false);
        launchParticleEffect.Play();
        Destroy(launchParticleEffect.gameObject, launchParticleEffect.main.duration);
    }
    

    protected void ImpactFeedback()
    {
        AudioSource[] sceneSources = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sceneSources.Length; i++)
        {
            Debug.Log("This Source Is: " + sceneSources[i]);
            Destroy(sceneSources[i].gameObject);
        }

        AudioHelper.PlayClip2D(_onHitSound, "Impact Sound: " + _tc.newProjectile.name, .1f, _onHitSound.length);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponentInChildren<Collider>();

        ParticleSystem impactSystem = Instantiate(_onHitEffect, _tc.newProjectile.transform, false);
        impactSystem.Play();

        mesh.enabled = false;
        col.enabled = false;
        Destroy(_tc.newProjectile, 3f);
    }

    

}
