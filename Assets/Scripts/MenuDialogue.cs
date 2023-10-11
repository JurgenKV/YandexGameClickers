using System;
using UnityEngine;


public class MenuDialogue : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _panel;
    
    public void OpenDialogue()
    {
        _panel.SetActive(true);
        if (_gameController.IsGameStarted)
        {
            _gameController.IsOnPause = true;
        }
    }

    public void CloseDialogue()
    {
        _panel.SetActive(false);
        if (_gameController.IsGameStarted)
        {
            _gameController.IsOnPause = false;
        }
    }
}