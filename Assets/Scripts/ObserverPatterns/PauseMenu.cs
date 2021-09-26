using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] GameController _gameController = null;

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
    void OnPauseGame()
    {
        // Display Pause Menu!
    }
    
}
