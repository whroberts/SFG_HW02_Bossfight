using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargeAttackBar : MonoBehaviour
{
    [SerializeField] Slider _chargeValueBar = null;
    [SerializeField] TurretController _tc = null;
    [SerializeField] TMP_Text _chargeTextValue = null;

    private ChargeOrb[] _chargeOrb;
    Image[] _sliderFill;

    public TurretController TurretController => _tc;
    public ChargeOrb[] ChargeOrbs => _chargeOrb;


    private void Awake()
    {
        _chargeValueBar.maxValue = 3;
        _chargeValueBar.value = 0;
        _chargeTextValue.text = "0";
    }

    private void OnEnable()
    {
        _chargeOrb = _tc.GetComponentsInChildren<ChargeOrb>();
        _sliderFill = _chargeValueBar.GetComponentsInChildren<Image>();

        _chargeOrb[_chargeOrb.Length-1].Charging += OnChargeOrb;
        _chargeValueBar.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _chargeOrb[_chargeOrb.Length - 1].Charging -= OnChargeOrb;
        _chargeValueBar.value = 0;
        _chargeTextValue.text = "0";
        _chargeOrb = null;
        _sliderFill[1].color = Color.red;
        _chargeValueBar.gameObject.SetActive(false);
    }

    void OnChargeOrb()
    {
        _chargeValueBar.value = _chargeOrb[_chargeOrb.Length - 1].OrbSize; 
        _chargeTextValue.text = _chargeOrb[_chargeOrb.Length - 1].OrbSize.ToString();
        ColorCheck();
    }

    void ColorCheck()
    {
        if (_chargeOrb[_chargeOrb.Length - 1].OrbSize < 1f)
        {
            _sliderFill[1].color = Color.red;
        }
        else
        {
            _sliderFill[1].color = new Color(0.627451f, 0.1254902f, 0.9411765f);
        }
    }
    
}
