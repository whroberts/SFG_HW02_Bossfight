using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// access actions
using System;

public class GameController : MonoBehaviour
{
    // subject that will notify observers
    public event Action<bool> PauseGame = delegate { };
    [SerializeField] PlayerHealth _ph = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadScene(0);
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

    public void TimeScale(int scale)
    {
        Time.timeScale = scale;
    }

    public void ReloadScene(int scene)
    {
        _ph.GodModeContainer(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
