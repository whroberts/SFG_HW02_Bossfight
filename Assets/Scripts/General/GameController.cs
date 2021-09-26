using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// access actions
using System;

public class GameController : MonoBehaviour
{
    // subject that will notify observers
    public event Action PauseGame = delegate { };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(0);
        } 
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
