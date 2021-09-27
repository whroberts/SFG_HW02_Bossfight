using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar = null;
    [SerializeField] BossHealth _bossHealth = null;
    [SerializeField] TMP_Text _healthTextValue = null;

    //public BossHealth BossHealth { get; private set; }

    public BossHealth BossHealth => _bossHealth;

    private void Awake()
    {
        //BossHealth = GetComponentInParent<BossHealth>();

        _healthBar.maxValue = BossHealth.MaxHealth;
        _healthBar.value = BossHealth.StartingHealth;
        _healthTextValue.text = Mathf.Round(BossHealth.StartingHealth).ToString() + "/" + BossHealth.MaxHealth;
    }

    private void OnEnable()
    {
        BossHealth.Damaged += OnTakeDamage;
    }

    private void OnDisable()
    {
        BossHealth.Damaged -= OnTakeDamage;
    }

    void OnTakeDamage(float damage)
    {
        _healthBar.value = BossHealth.CurrentHealth;
        _healthTextValue.text = Mathf.Round(BossHealth.CurrentHealth).ToString() + "/" + BossHealth.MaxHealth;
    }
}
