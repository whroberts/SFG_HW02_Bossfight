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
    [SerializeField] bool _burstAllowed = true;

    [Header("Override")]
    [SerializeField] bool _forceBurst = false;

    [Header("Charge Sound")]
    [SerializeField] private AudioClip _burstAudio = null;
    public AudioClip ChargeAudio => _burstAudio;
    
    AudioSource chargeAudio;

    private GameObject[] _newRocks;
    public GameObject[] NewRocksObject => _newRocks;

    private GameObject[] _newSawBlades;
    public GameObject[] NewSawBlades => _newSawBlades;

    private int _burstSawBlades;
    private bool _burst = false;
    public bool Burst => _burst;

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
                if (_forceBurst)
                {
                    _burst = true;
                    _burstSawBlades = Random.Range(10, 20);
                }
                else if (!_forceBurst)
                {
                    if (Random.Range(1, 5) == 3 && _burstAllowed)
                    {
                        _burst = true;
                        _burstSawBlades = Random.Range(10, 20);
                    }
                }

                if (_burst)
                {
                    StartCoroutine(BurstChargeAttack());
                }
                else if (!_burst)
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
    }
    public IEnumerator BurstChargeAttack()
    {

        AudioHelper.PlayClip2D(_burstAudio, "Burst: " + gameObject.name.ToString(), 0.5f, _burstAudio.length / 2f, 0f); 
        yield return new WaitForSeconds(_burstAudio.length / 2f);

        int numSawBlades = _burstSawBlades;
        _newSawBlades = new GameObject[numSawBlades];
        

        for (int i = 0; i < _newSawBlades.Length; i++)
        {
            _sawBladeArm.transform.LookAt(_player.transform);
            Quaternion playerLocation = _sawBladeArm.transform.rotation;
            _sawBladeArm.rotation = playerLocation * Quaternion.Euler(0f, Random.Range(-25f, 25f), 0f);


            _newSawBlades[i] = Instantiate(_sawBlade);
            _newSawBlades[i].name = "Sawblade: " + i.ToString();

            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));

            if (_bossController._bossTeleport.IsTeleporting)
            {
                StopAllCoroutines();
                break;
            }
        }
        _burst = false;
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
