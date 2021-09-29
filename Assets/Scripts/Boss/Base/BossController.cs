using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] float _eventDelay = 1f;

    [HideInInspector] public BossTeleport _bossTeleport;
    BossWeaponController _bossWeaponController;

    public float _lastEvent = 2f;
    public bool _attacking = false;

    private void Awake()
    {
        _bossTeleport = GetComponent<BossTeleport>();
        _bossWeaponController = GetComponent<BossWeaponController>();
    }

    private void Update()
    {
        TimeSinceLastEvent();
    }

    public void EventRandomization()
    {
        float randomizeAttack = Random.Range(0f, 1f);

        if ((randomizeAttack <= 1.0f) && (randomizeAttack > 0.75f))
        {
            StartCoroutine(_bossWeaponController.SawBladeAttack());
        }
        else if ((randomizeAttack <= 0.75f) && (randomizeAttack > 0.5))
        {
            _bossWeaponController.Rocket();
        }
        else if ((randomizeAttack <= 0.5f) && (randomizeAttack > 0.25f) && !_bossTeleport.IsTeleporting)
        {
            StartCoroutine(_bossTeleport.Teleport());
        }
        else if ((randomizeAttack <= 0.25f) && (randomizeAttack >= 0.0f))
        {
            StartCoroutine(_bossWeaponController.RocksAttack());
        }
        else
        {
            _bossWeaponController.Rocket();
        }
    }

    void TimeSinceLastEvent()
    {
        if (Time.time - _lastEvent >= _eventDelay)
        {
            if (!_attacking)
            {
                EventRandomization();
                _attacking = true;
            }
        }
    }
}
