﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject _sawBlade;
    [SerializeField] GameObject _rocket;
    [SerializeField] public GameObject _rocketArm;
    [SerializeField] GameObject _rock;

    [Header("Transforms")]
    [SerializeField] public Transform _player;
    [SerializeField] public Transform _sawBladeArm;
    [SerializeField] public Transform _rocketArmBarrel;
    [SerializeField] public Transform _launcher;

    public void SawBladeAttack()
    {
        GameObject sawBlade = Instantiate(_sawBlade);
    }

    public IEnumerator RocksAttack()
    {
        for (int i = 0; i < Random.Range(2,10); i++)
        {
            GameObject rock = Instantiate(_rock);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Rocket()
    {
        GameObject rocket = Instantiate(_rocket);
    }
}