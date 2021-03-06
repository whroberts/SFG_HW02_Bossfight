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
    [SerializeField] TMP_Text _incrementalDamageValue = null;

    //public BossHealth BossHealth { get; private set; }

    public BossHealth BossHealth => _bossHealth;

    float _tempDamage = 0;
    float _storedDamage = 0;

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
        StopAllCoroutines();
        ShowIncrement(damage);
    }

    private void ShowIncrement(float damage)
    {
        _tempDamage = _storedDamage + damage;
        _incrementalDamageValue.gameObject.SetActive(true);
        _incrementalDamageValue.text = "- " + Mathf.Round(_tempDamage).ToString();
        _storedDamage = _tempDamage;
        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(1f);
        _incrementalDamageValue.text = "";
        _incrementalDamageValue.gameObject.SetActive(false);
        _storedDamage = 0;
        _tempDamage = 0;
    }
}
