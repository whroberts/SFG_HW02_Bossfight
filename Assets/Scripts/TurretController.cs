using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] GameObject _missle;

    Transform _turret;

    public GameObject newProjectile;
    public bool _loaded = false;

    private void Awake()
    {
        _turret = this.gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadProjectile(_missle);
        }
    }
    private void LoadProjectile(GameObject projectile)
    {
        Debug.Log("Load Missle");
        newProjectile = Instantiate(projectile, _turret, false);
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
