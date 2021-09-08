using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int _maxHealth = 5;
    [SerializeField] int _currentHealth = 0;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth(int damageValue)
    {
        _currentHealth -= damageValue;
        HealthCheck();
    }

    private void HealthCheck()
    {
        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
