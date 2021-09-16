﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    private int _maxHealth = 100;
    public int _currentHealth = 0;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Boss health: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
