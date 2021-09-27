using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected abstract void ShootProjectile(GameObject projectile);

    [Header("Stats")]
    [SerializeField] protected int _damageValue = 1;
    [SerializeField] protected float _travelSpeed = 10f;

    [Header("Standard Effects")]
    [SerializeField] ParticleSystem _launchEffect = null;
    [SerializeField] ParticleSystem _onHitEffect = null;
    [SerializeField] AudioClip _launchSound = null;
    [SerializeField] AudioClip _onHitSound;

    protected TurretController _tc;
    private ChargeAttackBar _chargeBar;
    public ChargeAttackBar ChargeBar => _chargeBar;

    private void Awake()
    {
        _tc = FindObjectOfType<TurretController>();
        _chargeBar = FindObjectOfType<ChargeAttackBar>();
    }

    private void Start()
    {
        ShootProjectile(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        PlayerDetection playerDetection = collision.gameObject.GetComponent<PlayerDetection>();


        // insures that the projectiles shot from the player do not hit the player
        // or the collision box for the boss detection
        if (playerDetection == null && player == null)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                // calls the damage function in the object that was hit
                damageable.TakeDamage(_damageValue);

                //plays impact feedback for hitting a damageable object
                ImpactFeedback();
            }
            ImpactFeedback();
        }
    }

    // function for the charge orb
    // almost a direct copy from the OnCollisionEnter
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        PlayerDetection playerDetection = other.gameObject.GetComponent<PlayerDetection>();

        if (playerDetection == null && player == null)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                /*
                // only difference than the OnCollisionEvent
                // take damage is scaled to an integer value (will probably be changed),
                // of the current scale of the charge orb
                */
                damageable.TakeDamage(_damageValue * gameObject.transform.localScale.x);
                ImpactFeedback();
            }
        }
    }

    protected void LaunchFeedback()
    {
        if (_launchSound != null)
        {
            AudioHelper.PlayClip2D(_launchSound, "Launch Feedback: " + _tc.newProjectile.name, .01f, _launchSound.length);
        }

        if (_launchEffect != null)
        {
            ParticleSystem launchParticleEffect = Instantiate(_launchEffect, _tc.transform, false);
            launchParticleEffect.Play();
            Destroy(launchParticleEffect.gameObject, launchParticleEffect.main.duration);
        }
    }
    

    protected void ImpactFeedback()
    {
        // declares an audio source array and finds all of the current audio sources in the game
        AudioSource[] sceneSources = FindObjectsOfType<AudioSource>();

        for (int i = sceneSources.Length-1; i >= 0; i--)
        {
            //destoys all current audio sources
            Destroy(sceneSources[i].gameObject);
        }

        if (_onHitSound != null)
        {
            //calls the audio helper and launces an audio source object
            //adds a name to the object
            AudioHelper.PlayClip2D(_onHitSound, "Impact Sound: " + _tc.newProjectile.name, .01f, _onHitSound.length);
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        // stops the movement of the projectile
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;

        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Collider col = GetComponentInChildren<Collider>();

        if (_onHitEffect != null)
        {
            ParticleSystem impactSystem = Instantiate(_onHitEffect, _tc.newProjectile.transform, false);
            impactSystem.Play();
        }

        mesh.enabled = false;
        col.enabled = false;

        Destroy(gameObject, 3f);
    }

    

}
