using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject _missile;
    [SerializeField] GameObject _chargeOrb;

    Transform _turret;

    public GameObject newProjectile;
    public bool _loaded = false;

    float _beginCharge = 0f;
    float _timeCharged = 1f;

    private void Awake()
    {
        _turret = this.gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //LoadProjectile(_missile);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _beginCharge = Time.time;
            print(_beginCharge);

        } else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _timeCharged = Time.time - _beginCharge;
            print(_timeCharged);
            ChargeProjectile(_chargeOrb);
        }
    }
    private void LoadProjectile(GameObject projectile)
    {
        Debug.Log("Load Projectile");
        newProjectile = Instantiate(projectile, _turret, false);

        newProjectile.transform.localScale = Vector3.one * _timeCharged;

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = _turret.forward;
        _loaded = true;                 
        LoadFeedback();
    }

    private void ChargeProjectile(GameObject projectile)
    {
        Debug.Log("Charging Projectile");
        newProjectile = Instantiate(projectile, _turret, false);

        newProjectile.transform.localScale = Vector3.one * _timeCharged * 2f;

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = _turret.forward;
        _loaded = true;
        LoadFeedback();

    }

    private void LoadFeedback()
    {
        //play effects
    }
}
