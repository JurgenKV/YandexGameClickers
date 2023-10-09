using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        //CheckProgress();
        if(SceneManager.GetActiveScene().name.Equals("MainMenu"))
            YGRewardedVideoManager.ShowFullAds();
    }

    public static void SetLeaderboard(int num)
    {
        try
        {
            YandexGame.NewLeaderboardScores("BestLevelPlayerPusheenClicker", num);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
}