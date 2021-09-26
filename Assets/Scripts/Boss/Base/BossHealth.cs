using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class BossHealth : MonoBehaviour, IDamageable
{
    public event Action<int> Damaged = delegate { };
//    public event Action<int> Healed = delegate { };
    public event Action Killed = delegate { };

    [SerializeField] ParticleSystem _deathEffect;
    [SerializeField] int _startingHealth = 100;
    public int StartingHealth => _startingHealth;

    [SerializeField] int _maxHealth = 100;
    public int MaxHealth => _maxHealth;

    private int _currentHealth = 0;
    public int CurrentHealth
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

    public void TakeDamage(int amount)
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
