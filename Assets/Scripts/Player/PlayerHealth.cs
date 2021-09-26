using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for Actions
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<int> TookDamage = delegate { };

    [SerializeField] ParticleSystem _deathEffect;
    private int _maxHealth = 100;
    public int _currentHealth = 0;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        TookDamage?.Invoke(amount);

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
