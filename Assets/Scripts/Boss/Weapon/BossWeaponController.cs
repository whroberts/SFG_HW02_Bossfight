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

    private GameObject[] _newRocks;
    public GameObject[] NewRocksObject => _newRocks;

    private GameObject[] _newSawBlades;
    public GameObject[] NewSawBlades => _newSawBlades;

    BossController _bossController;

    private void Awake()
    {
        _bossController = GetComponent<BossController>();
    }

    public IEnumerator SawBladeAttack()
    {
        if (_player != null && !_bossController._bossTeleport.IsTeleporting)
        {
            if (_isSawBlade)
            {
                int numSawBlades = Random.Range(1, 5);
                _newSawBlades = new GameObject[numSawBlades];

                for (int i = 0; i < _newSawBlades.Length; i++)
                {
                    _sawBladeArm.LookAt(_player.transform);
                    _newSawBlades[i] = Instantiate(_sawBlade);
                    _newSawBlades[i].name = "Sawblade: " + i.ToString();
                    yield return new WaitForSeconds(Random.Range(0.5f, 1f));

                    if (_bossController._bossTeleport.IsTeleporting)
                    {
                        StopAllCoroutines();
                        break;
                    }
                }
            }
        }
    }

    public IEnumerator RocksAttack()
    {
        if (_player != null && !_bossController._bossTeleport.IsTeleporting)
        {
            if (_isRock)
            {
                int numRocks = Random.Range(8, 15);
                _newRocks = new GameObject[numRocks];

                for (int i = 0; i < _newRocks.Length; i++)
                {
                    _launcher.gameObject.transform.localRotation = Quaternion.Euler(15f, 0f, Random.Range(-3f, 3f));
                    yield return new WaitForSeconds(0.25f);
                    _newRocks[i] = Instantiate(_rock);
                    _newRocks[i].name = "Rock: " + i.ToString();
                    yield return new WaitForSeconds(Random.Range(.2f, 0.3f));

                    if (_bossController._bossTeleport.IsTeleporting)
                    {
                        StopAllCoroutines();
                        break;
                    }
                }
            }
        }
    }

    public void Rocket()
    {
        if (_player != null  && !_bossController._bossTeleport.IsTeleporting)
        {
            if (_isRocket)
            {
                GameObject rocket = Instantiate(_rocket);

                if (_bossController._bossTeleport.IsTeleporting)
                {
                    StopAllCoroutines();
                }
            }
        }
    }
}
