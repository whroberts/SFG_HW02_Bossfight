using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar = null;
    [SerializeField] PlayerHealth _playerHealth = null;
    [SerializeField] TMP_Text _healthTextValue = null;

    //PlayerHealth PlayerHealth { get; private set; }
    public PlayerHealth PlayerHealth => _playerHealth;

    private void Awake()
    {
        //PlayerHealth = GetComponentInParent<PlayerHealth>();

        _healthBar.maxValue = PlayerHealth.MaxHealth;
        _healthBar.value = PlayerHealth.StartingHealth;
        _healthTextValue.text = Mathf.Round(PlayerHealth.StartingHealth).ToString() + "/" + PlayerHealth.MaxHealth;
    }

    private void OnEnable()
    {
        PlayerHealth.Damaged += OnTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.Damaged -= OnTakeDamage;
    }

    void OnTakeDamage(float damage)
    {
        _healthBar.value = PlayerHealth.CurrentHealth;
        _healthTextValue.text = Mathf.Round(PlayerHealth.CurrentHealth).ToString() + "/" + PlayerHealth.MaxHealth;
    }
}
