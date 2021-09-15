using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeath : MonoBehaviour, IDamageable
{
    private int _health = 100;

    public void TakeDamage(int amount)
    {
        Debug.Log("You hurt the boss");
        _health -= amount;
        Debug.Log("Boss health: " + _health);
    }
}
