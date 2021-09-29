using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for Actions
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<float> Damaged = delegate { };
    public event Action Camera = delegate { };
    public event Action Killed = delegate { };

    [SerializeField] ParticleSystem _deathEffect = null;
    [SerializeField] float _startingHealth = 100;

    public float StartingHealth => _startingHealth;

    [SerializeField] float _maxHealth = 100;
    public float MaxHealth => _maxHealth;

    [SerializeField] public bool _godMode = false;

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
        if (!_godMode)
        {
            _currentHealth -= amount;
            Damaged?.Invoke(amount);
            Camera?.Invoke();

            if (_currentHealth <= 0)
            {
                Killed?.Invoke();
                Kill();
            }
        }
    }

    public void GodModeContainer(bool state)
    {
        _godMode = SetGodMode(state);
    }

    private bool SetGodMode(bool state)
    {
        _godMode = state;
        return _godMode;
    }

    private void Kill()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);

        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        Rigidbody rb = GetComponent<Rigidbody>();
        TurretController tc = GetComponentInChildren<TurretController>();

        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        tc.enabled = false;

        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = false;
        }
    }
}
