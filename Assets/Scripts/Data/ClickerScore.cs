using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using YG;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;


public class ClickerScore : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private TMP_Text clicksUI;
    [SerializeField] private TMP_Text lvlUI;
    
    [Header("Upgrade Click")]
    [SerializeField] private TMP_Text upgradeClickCostUI;

    [SerializeField] private double constX;
    [SerializeField] private double degreeY;
    
    [Space(1)]
    [SerializeField] private Button _buttonX2;
    [SerializeField] private Button _buttonUpdate;

    public long ClicksCount = 0;
    private bool _coroutineX2CLicks = false;
    public int ClickMultiplayer = 1;

    [SerializeField] public List<GameObject> ParticleSystems;
    //[SerializeField] private List<Animator> _animators;
    private void Start()
    {
        UpdateUpgradeClickUI();
        UpdateLeaderboard();
    }

    private void Update()
    {
        clicksUI.text = ClicksCount.ToString() + "$";
        lvlUI.text = ClickMultiplayer.ToString();
    }

    public void Click()
    {
        if(!gameObject.activeSelf)
            return;

        if (_coroutineX2CLicks)
        {
            ClicksCount += 1 * ClickMultiplayer * 2;
        }
        else
        {
            ClicksCount += 1 * ClickMultiplayer;
        }

        YandexGame.savesData.Score = ClicksCount;
        YandexGame.SaveProgress();
    }

    public void VideoClickX2()
    {
        if (!_coroutineX2CLicks)
        {
            StartCoroutine(TimerX2Coroutine());
            YandexGame.RewVideoShow(1);
            
        }
           
    }

    IEnumerator TimerX2Coroutine()
    {
        _coroutineX2CLicks = true;
        _buttonX2.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonX2.interactable = true;
        _coroutineX2CLicks = false;
    }

    public void UpgradeClick()
    {
         if (ClicksCount < GetUpgradeCost())
             return;
        
         ClicksCount -= GetUpgradeCost();
        
        ClickMultiplayer++;
        UpdateUpgradeClickUI();
        YandexGame.savesData.ScoreMultiplayer = ClickMultiplayer;
        UpdateLeaderboard();
        YandexGame.SaveProgress();
    }

    private void UpdateUpgradeClickUI()
    {
        upgradeClickCostUI.text = GetUpgradeCost().ToString() + "$";
        RefreshEgg();
    }

    private void RefreshEgg()
    {
        if(ClickMultiplayer < 10)
            YandexGame.savesData.ObjectImageSecNum = ClickMultiplayer - 1;
        else
            YandexGame.savesData.ObjectImageSecNum = -1;
        
        if (ClickMultiplayer == 10 & !YandexGame.savesData.IsAnimal)
        {
            YandexGame.savesData.AnimalNum = Random.Range(0, _gameController._animalsSprites.Count);
            YandexGame.savesData.IsAnimal = true;
        }
        YandexGame.SaveProgress();
        _gameController.CheckProgress();
    }
    
    
    private long GetUpgradeCost()
    {
        //x * ((level+1) ^ y) - (x * level):
        double cost = ((constX * Math.Pow((ClickMultiplayer+1), degreeY)) - (constX * (ClickMultiplayer+1)));
        Debug.Log("cost = " + cost);
        
        
        return (long)cost;
    }
    
    public void ADSUpgradeClick()
    {
        ClickMultiplayer++;
        UpdateUpgradeClickUI();
        StartCoroutine(TimerUpdateCoroutine());
        try
        {
            YandexGame.RewVideoShow(2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        YandexGame.savesData.ScoreMultiplayer = ClickMultiplayer;
        UpdateLeaderboard();
        YandexGame.SaveProgress();
    }
    IEnumerator TimerUpdateCoroutine()
    {
        _buttonUpdate.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonUpdate.interactable = true;
    }

    private void UpdateLeaderboard()
    {
        try
        {
            YandexGame.NewLeaderboardScores("BestLevelPlayer", ClickMultiplayer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}