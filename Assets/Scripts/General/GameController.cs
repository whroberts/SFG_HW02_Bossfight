using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// access actions
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _deathScreen = null;

    PlayerHealth _playerHealth;

    // subject that will notify observers
    public event Action<bool> PauseGame = delegate { };

    private void Awake()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnEnable()
    {
        _playerHealth.Killed += OnKilled;
    }

    private void OnDisable()
    {
        _playerHealth.Killed -= OnKilled;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(0);
        } 
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame?.Invoke(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void OnKilled()
    {
        if (_deathScreen != null) 
        { 

        }

    }

    /*
    public void Pause()
    {

        //notify the observers!
        PauseGame?.Invoke();
    }
    */

}
