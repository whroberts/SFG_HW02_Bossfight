using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject _missile;
    [SerializeField] GameObject _chargeOrb;

    [Header("Effects")]
    [SerializeField] AudioClip _orbChargeSound;

    Transform _turret;
    AudioSource chargeAudio;

    ProjectileBase _pb;

    public GameObject newProjectile;
    public GameObject newOrb;

    float _beginCharge = 0f;
    public float _timeCharged = 1f;

    float _lastShot;


    private void Awake()
    {
        _turret = this.gameObject.transform;
        _pb = FindObjectOfType<ProjectileBase>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LoadProjectile(_missile);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _beginCharge = Time.time;
            chargeAudio = AudioHelper.PlayClip2D(_orbChargeSound, "Charge Audio: " + _chargeOrb.name, 0.1f, 15f);

        } else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _timeCharged = Time.time - _beginCharge;

            Destroy(chargeAudio.gameObject);
            LoadProjectile(_chargeOrb);
        }
    }
    private void LoadProjectile(GameObject projectile)
    {
        newProjectile = Instantiate(projectile, _turret, false);

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward;
    }
}
