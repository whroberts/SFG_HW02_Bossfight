using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : BossWeaponBase
{
    protected override void Attack(GameObject weapon)
    {
        weapon.transform.position = _bc._sawBladeArm.position;
        weapon.transform.LookAt(_bc._player);
        _rb.velocity = transform.forward * _launchSpeed;
    }

    protected override void RotateEvent()
    {
        base.RotateEvent();
        _rb.transform.Rotate(Vector3.right * _rotationStep * _launchSpeed * Time.deltaTime);
    }
}
