using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BossWeaponBase
{
    bool _launched = false;

    protected override void Attack(GameObject rocket)
    {
        if (!_launched)
        {
            ShootRocket(rocket);
        }
    }

    protected override void RotateEvent()
    {
        _bc._rocketArm.transform.LookAt(_bc._player);
    }

    private void ShootRocket(GameObject rocket)
    {
        _rb.transform.position = _bc._rocketArmBarrel.position;
        _rb.transform.rotation = _bc._rocketArmBarrel.rotation;
        _rb.velocity = transform.forward * _launchSpeed;
        _launched = true;
    }

    private void OnDestroy()
    {
        _launched = false;
        _bossController._attacking = false;
    }
}
