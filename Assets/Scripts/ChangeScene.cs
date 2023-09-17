using System;
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
        YandexGame.savesData.Score = 0;
        YandexGame.savesData.ScoreMultiplayer = 1;
        YandexGame.savesData.BgNum = -1;
        YandexGame.savesData.GirlNumber = 0;
        YandexGame.savesData.IsTestCompleted = false;
        YandexGame.savesData.IsDateStarted = false;
        YandexGame.savesData.IsGirlUndressed = false;
        UpdateLeaderboard();
        YandexGame.SaveProgress();
    }
    
    private void UpdateLeaderboard()
    {
        try
        {
            YandexGame.NewLeaderboardScores("BestLevelPlayer", 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}