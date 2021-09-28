using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for Actions
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<float> Damaged = delegate { };
//    public event Action<int> Healed = delegate { };
    public event Action Killed = delegate { };

    [SerializeField] ParticleSystem _deathEffect = null;
    [SerializeField] float _startingHealth = 100;
    public float StartingHealth => _startingHealth;

    [SerializeField] float _maxHealth = 100;
    public float MaxHealth => _maxHealth;

    private float _currentHealth = 0;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value > _maxHealth)
            {
                value = _maxHealth;
            }
            _currentHealth = value;
        }
    }

    private void Start()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        Damaged?.Invoke(amount);

        if (_currentHealth <= 0)
        {
            Kill();
            Killed?.Invoke();
        }
    }

    private void Kill()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
