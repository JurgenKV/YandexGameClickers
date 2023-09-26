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
    [SerializeField] private ProgressUI _progressUI;
    
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
    
    [SerializeField]private int _level = 0;
    [SerializeField]private long _experience;
    [SerializeField]private long _experienceToNextLevel;

    [SerializeField] public List<GameObject> ParticleSystems;
    //[SerializeField] private List<Animator> _animators;
    private void Start()
    {
        LoadData();
        
        if(_level == 0)
            SetLevel(1);
        
        UpdateUpgradeClickUI();
        UpdateLeaderboard();
        _progressUI.RefreshAllUI();
    }

    private void LoadData()
    {
        ClicksCount = YandexGame.savesData.MoneyAmount;
        ClickMultiplayer = YandexGame.savesData.ClickMultiplayer;
        _level = YandexGame.savesData.Level;
        _experience = YandexGame.savesData.Experience;
        _experienceToNextLevel = YandexGame.savesData.ExperienceToNextLevel;
    }

    public void Click()
    {
        if(!gameObject.activeSelf)
            return;

        if (_coroutineX2CLicks)
        {
            ClicksCount += 1 * ClickMultiplayer * 2;
            AddExperience(1 * ClickMultiplayer * 2);
        }
        else
        {
            ClicksCount += 1 * ClickMultiplayer;
            AddExperience(1 * ClickMultiplayer);
        }
        Debug.Log("Click");
        YandexGame.savesData.MoneyAmount = ClicksCount;
        YandexGame.SaveProgress();
        _progressUI.RefreshAllUI();
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
        YandexGame.savesData.ClickMultiplayer = ClickMultiplayer;
        UpdateLeaderboard();
        YandexGame.SaveProgress();
    }

    private void UpdateUpgradeClickUI()
    {
        upgradeClickCostUI.text = GetUpgradeCost().ToString() + "$";
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
        YandexGame.savesData.ClickMultiplayer = ClickMultiplayer;
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
            YandexGame.NewLeaderboardScores("BestLevelPlayerPusheenClicker", 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    public void AddExperience(long experienceToAdd)
    {
        _experience += experienceToAdd;
        
        if (_experience >= _experienceToNextLevel)
        {
            SetLevel(_level + 1);
        }
    }

    private void SetLevel(int value)
    {
        _level = value;
        _experience = _experience - _experienceToNextLevel;
        _experienceToNextLevel = (int)(50f * (Mathf.Pow(_level + 1, 2) - (5 * (_level + 1)) + 8));
        UpdateVisual();
        
        YandexGame.savesData.Level = _level;
        YandexGame.savesData.Experience = _experience;
        YandexGame.savesData.ExperienceToNextLevel = _experienceToNextLevel;
        YandexGame.SaveProgress();
        
        _progressUI.RefreshAllUI();
    }

    public void UpdateVisual()
    {
        Debug.Log(_level.ToString("0") + "\nto next lvl: " + _experienceToNextLevel + "\ncurrent exp: " + _experience);
        //lvlUI.text = _level.ToString();
    }
}