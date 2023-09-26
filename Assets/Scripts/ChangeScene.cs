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
    
    public void RestartGame()
    {
        ResetPlayerData();
        ChangeSceneButton();
    }

    private void ResetPlayerData()
    {
        YandexGame.savesData.MoneyAmount = 0;
        YandexGame.savesData.ClickMultiplayer = 1;
        YandexGame.savesData.BgNum = -1;
        YandexGame.savesData.Experience = 0;
        YandexGame.savesData.ExperienceToNextLevel = 200;
        YandexGame.savesData.Level = 1;
        YandexGame.savesData.PusheenNums = new List<int>();
        ResetLeaderboard();
        YandexGame.SaveProgress();
        Debug.Log("ResetPlayerData");
    }
    
    private void ResetLeaderboard()
    {
        try
        {
            YandexGame.NewLeaderboardScores("BestLevelPlayerPusheenClicker", 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}