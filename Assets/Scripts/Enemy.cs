using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int _maxHealth = 2;
    int _currentHealth = 0;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
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
