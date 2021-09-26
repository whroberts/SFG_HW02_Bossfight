using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar = null;
    [SerializeField] PlayerHealth _playerHealth = null;

    //PlayerHealth PlayerHealth { get; private set; }
    public PlayerHealth PlayerHealth => _playerHealth;

    private void Awake()
    {
        //PlayerHealth = GetComponentInParent<PlayerHealth>();

        _healthBar.maxValue = PlayerHealth.MaxHealth;
        _healthBar.value = PlayerHealth.StartingHealth;
    }

    private void OnEnable()
    {
        PlayerHealth.Damaged += OnTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.Damaged -= OnTakeDamage;
    }

    void OnTakeDamage(int damage)
    {
        _healthBar.value = PlayerHealth.CurrentHealth;
    }
}
