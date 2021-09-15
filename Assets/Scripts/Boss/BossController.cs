using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    BossMovement _bossMovement;
    BossWeaponController _bossWeaponController;


    private void Awake()
    {
        _bossMovement = GetComponent<BossMovement>();
        _bossWeaponController = GetComponent<BossWeaponController>();
    }

    protected void EventRandomization()
    {
        float randomizeAttack = Random.Range(0f, 1f);

        if ((randomizeAttack <= 1.0f) && (randomizeAttack > 0.8f))
        {
            Debug.Log("Initialize Saw Blade");
            _bossWeaponController.SawBladeAttack();
        }
        else if ((randomizeAttack <= 0.8f) && (randomizeAttack > 0.6))
        {
            Debug.Log("Initialize Rocket Launcher");
        }
        else if ((randomizeAttack <= 0.6f) && (randomizeAttack > 0.4f))
        {
            Debug.Log("Initialize Teleport");
            StartCoroutine(_bossMovement.Teleport());
        }
        else if ((randomizeAttack <= 0.2f) && (randomizeAttack >= 0.0f))
        {
            Debug.Log("Initialize Falling Rocks");
            _bossWeaponController.RockAttack();

        }
    }

    public void EventTest()
    {
        bool x = true;

        if (x)
        {
            //Debug.Log("Initialize Saw Blade");
            //_bossWeaponController.SawBladeAttack();
            _bossWeaponController.RockAttack();
        }

        if (!x)
        {
            Debug.Log("Initialize Teleport");
            StartCoroutine(_bossMovement.Teleport());
        }
    }
}
