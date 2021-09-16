using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BossWeaponBase
{
    protected override void Attack(GameObject weapon)
    {
        weapon.transform.position = _bc._launcher.position;
        weapon.transform.position = new Vector3(weapon.transform.position.x + Random.Range(-5f, 5f), 
            weapon.transform.position.y + Random.Range(-0.5f, 1f), weapon.transform.position.z + Random.Range(-2f, 2f));
        _rb.velocity = _bc._launcher.up * _launchSpeed * Random.Range(0.75f, 2f);

        Destroy(gameObject, 5f);
    }

    
    protected override void RotateEvent()
    {
        base.RotateEvent();
        _rb.transform.Rotate(Vector3.right * _rotationStep * _launchSpeed * Time.deltaTime);
    }
    
}
