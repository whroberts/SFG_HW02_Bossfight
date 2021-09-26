using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BossWeaponBase
{
    protected override void Attack(GameObject weapon)
    {
        StartCoroutine(ShootRocket(weapon));
    }

    protected override void RotateEvent()
    {
        base.RotateEvent();
        _bc._rocketArm.transform.LookAt(_bc._player);
    }

    IEnumerator ShootRocket(GameObject weapon)
    {
        weapon.transform.position = _bc._rocketArmBarrel.position;
        weapon.transform.LookAt(_bc._player);
        yield return new WaitForSeconds(2f);
        _rb.velocity = _bc._rocketArmBarrel.forward * _launchSpeed;
    }
}
