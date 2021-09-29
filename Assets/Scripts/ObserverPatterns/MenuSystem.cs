using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] GameController _gameController = null;

    [Header("UI Canvases")]
    [SerializeField] GameObject _playerHUD = null;
    [SerializeField] GameObject _pauseMenu = null;
    [SerializeField] GameObject _deathMenu = null;
    [SerializeField] GameObject _winMenu = null;

    [Header("Player Scripts")]
    [SerializeField] GameObject _player = null;

    [Header("Boss")]
    [SerializeField] GameObject _boss = null;

    TankController _tc;
    TurretController _trc;
    PlayerHealth _ph;

    BossController _bc;
    BossMovement _bm;
    BossWeaponController _bwc;
    BossHealth _bh;

    

    private void Awake()
    {
        _tc = _player.GetComponent<TankController>();
        _trc = _player.GetComponentInChildren<TurretController>();
        _ph = _player.GetComponent<PlayerHealth>();

        _bc = _boss.GetComponent<BossController>();
        _bm = _boss.GetComponent<BossMovement>();
        _bwc = _boss.GetComponent<BossWeaponController>();
        _bh = _boss.GetComponent<BossHealth>();
    }

    private void OnEnable()
    {
        //subscribe to event
        _gameController.PauseGame += OnPauseGame;
        _ph.Killed += OnKilled;
        _bh.Killed += OnWinGame;
    }

    private void OnDisable()
    {
        // unsubscribe to event
        _gameController.PauseGame -= OnPauseGame;
        _ph.Killed -= OnKilled;
        _bh.Killed -= OnWinGame;
    }

    // OnPauseGame is called on event notify
    void OnPauseGame(bool state)
    {
        StandardMenu();
        _pauseMenu.SetActive(true);
        _playerHUD.SetActive(false);
        PlayerInput(false);
        Time.timeScale = 0;
    }

    public void PlayerInput(bool state)
    {
        _tc.enabled = state;
        _trc.enabled = state;
        _ph.enabled = state;
        _bc.enabled = state;
        _bm.enabled = state;
        _bwc.enabled = state;
        _bh.enabled = state;
    }

    void OnKilled()
    {
        if (_deathMenu != null)
        {
            StandardMenu();
            _deathMenu.SetActive(true);
            PlayerInput(false);
            //Time.timeScale = 0;
        }

    }

    void OnWinGame()
    {
        if (_winMenu != null)
        {
            StandardMenu();
            _winMenu.SetActive(true);
            PlayerInput(false);
            //Time.timeScale = 0;
        }
    }

    void StandardMenu()
    {
        _bc.StopAllCoroutines();
        _bwc.StopAllCoroutines();
        _ph.GodModeContainer(true);

        AudioSource[] sceneSources = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sceneSources.Length; i++)
        {
            Destroy(sceneSources[i].gameObject);
        }
    }
}
