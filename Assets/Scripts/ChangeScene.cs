using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void ChangeSceneButton()
    {
        SceneManager.LoadScene(_sceneName);
    }
    
    public void ChangeSceneButtonWithAds()
    {
        SceneManager.LoadScene(_sceneName);
        GameController.ShowFullAds();
    }
    
    public void RestartGame()
    {
        SavesYG.ResetPlayerData();
        ChangeSceneButton();
    }
    
}