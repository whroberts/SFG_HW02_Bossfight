using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float _maxHealth = 20f;
    float _currentHealth = 0f;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        HealthCheck();
    }

    private void HealthCheck()
    {
        //check for death
        if (_currentHealth <= 0 )
        {
            Destroy(this.gameObject);
        }
    }
 }
