using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController: MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject _sawBlade;
    [SerializeField] GameObject _rocket;

    [Header("Transforms")]
    [SerializeField] public Transform _player;
    [SerializeField] public Transform _sawBladeArm;

    [Header("Effects")]
    [SerializeField] AudioClip _attackSound;

    public GameObject newWeapon;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            AttackTesting();
        }
    }

    public void InitializeBossEvent(GameObject action)
    {
        newWeapon = Instantiate(action);
    }

    private void AttackTesting()
    {
        Debug.Log("Initialize Saw Blade");
        InitializeBossEvent(_sawBlade);
    }

    private void AttackRandomization()
    {
        float randomizeAttack = Random.Range(0f, 1f);

        if ((randomizeAttack <= 1.0f) && (randomizeAttack > 0.8f))
        {
            Debug.Log("Initialize Saw Blade");
            InitializeBossEvent(_sawBlade);
        }
        else if ((randomizeAttack <= 0.8f) && (randomizeAttack > 0.6))
        {
            Debug.Log("Initialize Rocket Launcher");
            InitializeBossEvent(_rocket);
        }
        else if ((randomizeAttack <= 0.6f) && (randomizeAttack > 0.4f))
        {
            Debug.Log("Initialize Head Slam");
            //InitializeAttack(_headSlam);
        }
        else if ((randomizeAttack <= 0.4f) && (randomizeAttack > 0.2f))
        {
            Debug.Log("Initialize Floor Slide");
            //InitializeAttack(_floorSlide);
        }
        else if ((randomizeAttack <= 0.2f) && (randomizeAttack >= 0.0f))
        {
            Debug.Log("Initialize Falling Rocks");
            //InitializeAttack(_Falling Rocks);
        }
    }
}
