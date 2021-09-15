using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BossWeaponBase
{
    protected override void Attack(GameObject weapon)
    {
        weapon.transform.position = _bc._launcher.position;
        _rb.velocity = _bc._launcher.up * _launchSpeed;
    }

    
    protected override void RotateEvent()
    {
        base.RotateEvent();
        _rb.transform.Rotate(Vector3.right * _rotationStep * _launchSpeed * Time.deltaTime);
    }
    
}
