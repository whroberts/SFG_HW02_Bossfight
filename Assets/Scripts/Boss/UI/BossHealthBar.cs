﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar = null;
    [SerializeField] BossHealth _bossHealth = null;

    //public BossHealth BossHealth { get; private set; }

    public BossHealth BossHealth => _bossHealth;

    private void Awake()
    {
        //BossHealth = GetComponentInParent<BossHealth>();

        _healthBar.maxValue = BossHealth.MaxHealth;
        _healthBar.value = BossHealth.StartingHealth;
    }

    private void OnEnable()
    {
        BossHealth.Damaged += OnTakeDamage;
    }

    private void OnDisable()
    {
        BossHealth.Damaged -= OnTakeDamage;
    }

    void OnTakeDamage(int damage)
    {
        _healthBar.value = BossHealth.CurrentHealth;
    }
}