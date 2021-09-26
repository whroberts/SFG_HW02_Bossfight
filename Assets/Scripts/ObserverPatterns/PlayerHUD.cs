using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    
    [SerializeField] PlayerHealth _playerHealth = null;

    private void OnEnable()
    {
        _playerHealth.TookDamage += OnTookDamage;
    }

    private void OnDisable()
    {
        _playerHealth.TookDamage -= OnTookDamage;
    }

    void OnTookDamage(int damage)
    {
        // trigger combat text with damage amount   
    }
    
}
