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

    protected BossWeaponController _bc;
    protected Rigidbody _rb;

    private void Awake()
    {
        _bc = FindObjectOfType<BossWeaponController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Attack(_bc.newWeapon);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {

            Destroy(gameObject);
        }
    }

    protected virtual void RotateEvent()
    {
        Debug.Log("Rotate Event Function");
    }

    private void FixedUpdate()
    {
        RotateEvent();
    }
}
