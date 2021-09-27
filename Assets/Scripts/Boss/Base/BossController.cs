using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] float _eventDelay = 2f;

    BossMovement _bossMovement;
    BossWeaponController _bossWeaponController;

    public float _lastEvent;

    private void Awake()
    {
        _bossMovement = GetComponent<BossMovement>();
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
            _bossWeaponController.SawBladeAttack();
        }
        else if ((randomizeAttack <= 0.75f) && (randomizeAttack > 0.5))
        {
            _bossWeaponController.Rocket();
        }
        else if ((randomizeAttack <= 0.5f) && (randomizeAttack > 0.25f))
        {
             StartCoroutine(_bossMovement.Teleport());
        }
        else if ((randomizeAttack <= 0.25f) && (randomizeAttack >= 0.0f))
        {
            StartCoroutine(_bossWeaponController.RocksAttack());

        }
    }

    void TimeSinceLastEvent()
    {
        if (Time.time - _lastEvent >= _eventDelay)
        {
            //EventRandomization();
            _bossWeaponController.Rocket();
            _lastEvent = Time.time;
        }
    }
}
