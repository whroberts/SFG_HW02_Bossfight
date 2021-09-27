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

    public TurretController TurretController => _tc;

    //public ChargeOrbNew ChargeOrb => _chargeOrb;
    ChargeOrbNew _chargeOrb;

    /*
    private void Awake()
    {
        _chargeValueBar.maxValue = 2;
        _chargeValueBar.value = TurretController.BeginCharged;
        _chargeTextValue.text = TurretController.TimeCharged.ToString();
    }

    private void OnEnable()
    {
        TurretController.Charged += OnChargeOrb;
        Debug.Log("Enabled");
    }

    private void Update()
    {
        OnChargeOrb();
        Debug.Log("Update");
    }

    private void OnDisable()
    {
        TurretController.Charged -= OnChargeOrb;
        Debug.Log("Disabled");
    }

    void OnChargeOrb()
    {
        _chargeValueBar.value = TurretController.TimeCharged;
        _chargeTextValue.text = TurretController.TimeCharged.ToString();
    }
    */
    private void Awake()
    {
        _chargeOrb = FindObjectOfType<ChargeOrbNew>();

        _chargeValueBar.maxValue = 2;
        _chargeValueBar.value = 0;
        _chargeTextValue.text = "0";
    }

    /*
    private void OnEnable()
    {
        _chargeOrb.Charging += OnChargeOrb;
        Debug.Log("Enabled");
    }

    private void OnDisable()
    {
        _chargeOrb.Charging -= OnChargeOrb;
        Debug.Log("Disabled");
    }
    */

    void OnChargeOrb()
    {
        _chargeValueBar.value = _chargeOrb.OrbSize;
        _chargeTextValue.text = _chargeOrb.OrbSize.ToString();
    }
}
