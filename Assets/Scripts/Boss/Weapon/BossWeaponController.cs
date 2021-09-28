using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject _sawBlade = null;
    [SerializeField] GameObject _rocket = null;
    [SerializeField] public GameObject _rocketArm = null;
    [SerializeField] GameObject _rock = null;

    [Header("Transforms")]
    [SerializeField] public Transform _player = null;
    [SerializeField] public Transform _sawBladeArm = null;
    [SerializeField] public Transform _rocketArmBarrel = null;
    [SerializeField] public Transform _launcher = null;

    [Header("Enabled")]
    [SerializeField] bool _isSawBlade = true;
    [SerializeField] bool _isRocket = true;
    [SerializeField] bool _isRock = true;

    private GameObject[] rock;
    public GameObject[] RockObject => rock;

    BossController _bossController;

    private void Awake()
    {
        _bossController = GetComponent<BossController>();
    }

    public void SawBladeAttack()
    {
        if (_isSawBlade)
        {
            GameObject sawBlade = Instantiate(_sawBlade);
        }
    }

    public IEnumerator RocksAttack()
    {
        if (_isRock)
        {
            int numRocks = Random.Range(8, 15);
            rock = new GameObject[numRocks];

            for (int i = 0; i < rock.Length; i++)
            {
                _launcher.gameObject.transform.localRotation = Quaternion.Euler(15f, 0f, Random.Range(-3f, 3f));
                yield return new WaitForSeconds(0.25f);
                rock[i] = Instantiate(_rock);
                rock[i].name = "Rock: " + i.ToString();
                yield return new WaitForSeconds(Random.Range(.2f, 0.3f));
            }
        }
    }

    public void Rocket()
    {
        if (_isRocket)
        {
            GameObject rocket = Instantiate(_rocket);
        }
    }
}
