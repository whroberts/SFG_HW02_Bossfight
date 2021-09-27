using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class TurretController : MonoBehaviour
{
    public event Action Charged = delegate { };

    [Header("Weapons")]
    [SerializeField] GameObject _missile = null;
    [SerializeField] GameObject _chargeOrb = null;

    [Header("Effects")]
    [SerializeField] AudioClip _orbChargeSound = null;

    Transform _turret;

    AudioSource _chargeAudio;

    ProjectileBase _pb;

    public GameObject newProjectile;
    public GameObject newOrb;

    private bool _charging = false;
    public bool Charging => _charging;

    private float _beginCharge = 0f;
    public float BeginCharged => _beginCharge;

    private float _timeCharged = 0f;
    public float TimeCharged => _timeCharged;
    
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
            _chargeAudio = AudioHelper.PlayClip2D(_orbChargeSound, "Charge Audio: " + _chargeOrb.name, 0.1f, 15f);
            _charging = true;
            LoadProjectile(_chargeOrb);

        } else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _timeCharged = Time.time - _beginCharge;
            _charging = false;
            //Charged?.Invoke();
            Destroy(_chargeAudio.gameObject);

        }
    }
    private void LoadProjectile(GameObject projectile)
    {
        newProjectile = Instantiate(projectile, _turret, false);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward;
    }
}
