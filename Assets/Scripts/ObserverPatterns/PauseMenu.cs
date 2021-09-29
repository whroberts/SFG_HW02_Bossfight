using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameController _gameController = null;
    [SerializeField] Canvas _pauseScreen = null;
    [SerializeField] Canvas _playerHUD = null;

    [Header("Player Scripts")]
    [SerializeField] GameObject _player = null;

    [Header("Boss")]
    [SerializeField] GameObject _boss = null;

    TankController _tc;
    PlayerHealth _ph;

    BossController _bc;
    BossMovement _bm;
    BossWeaponController _bwc;
    BossHealth _bh;

    

    private void Awake()
    {
        _tc = _player.GetComponent<TankController>();
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
    }

    private void OnDisable()
    {
        // unsubscribe to event
        _gameController.PauseGame -= OnPauseGame;
    }

    // OnPauseGame is called on event notify
    void OnPauseGame(bool state)
    {
        Time.timeScale = 0;
        _pauseScreen.enabled = !state;
        _playerHUD.enabled = state;

        PlayerInput(state);

    }

    public void PlayerInput(bool state)
    {
        _tc.enabled = state;
        _ph.enabled = state;
        _bc.enabled = state;
        _bm.enabled = state;
        _bwc.enabled = state;
        _bh.enabled = state;
    }
    
}
