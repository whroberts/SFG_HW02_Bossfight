using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] Slider _healthBar = null;
    [SerializeField] PlayerHealth _playerHealth = null;
    [SerializeField] TMP_Text _healthTextValue = null;

    [Header("Damage Flash")]
    [SerializeField] Image _damageImage = null;

    [SerializeField] CameraController _cameraController = null;

    private float _effectsLength = 1f;
    private float _effectsStep;

    public float EffectsLength => _effectsLength;
    public float EffectsStep => _effectsStep;

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
        StartCoroutine(FlashDamageImage());
        StartCoroutine(_cameraController.CameraShake());
    }

    private IEnumerator FlashDamageImage()
    {
        _damageImage.gameObject.SetActive(true);

        _effectsStep = _effectsLength / (_damageImage.color.a * 255);

        yield return new WaitForSeconds(0.25f);

        for (int i = (int) (_damageImage.color.a * 255); i >= 0; i--)
        {
            _damageImage.color = new Color(_damageImage.color.r, _damageImage.color.g, _damageImage.color.b, (float)(i / 255f));
            yield return new WaitForSeconds(_effectsStep);
        }
        _damageImage.gameObject.SetActive(false);
        _damageImage.color = new Color(_damageImage.color.r, _damageImage.color.g, _damageImage.color.b, (104f / 255f));
    }
}
