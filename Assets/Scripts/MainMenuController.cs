using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;


public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        try
        {
            if(SceneManager.GetActiveScene().name.Equals("MainMenu") || SceneManager.GetActiveScene().name.Equals("Shop"))
                YGRewardedVideoManager.ShowFullAds();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        try
        {
            if(SceneManager.GetActiveScene().name.Equals("MainMenu"))
                GameController.SetLeaderboard((int)YandexGame.savesData.BestScore);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }
}