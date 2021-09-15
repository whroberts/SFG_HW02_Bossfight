using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController: MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] public GameObject _sawBlade;
    [SerializeField] public GameObject _rocket;
    [SerializeField] public GameObject _rock;

    [Header("Transforms")]
    [SerializeField] public Transform _player;
    [SerializeField] public Transform _sawBladeArm;
    [SerializeField] public Transform _launcher;

    public void SawBladeAttack()
    {
        GameObject sawBlade = Instantiate(_sawBlade);
    }

    public void RockAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject rock = Instantiate(_rock);
        }
    }
}
