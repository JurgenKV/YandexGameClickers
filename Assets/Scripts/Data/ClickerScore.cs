using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

    private long _clickCount = 0;
    public long ClicksCount
    {
        get
        {
            return _clickCount ; // возвращаем значение свойства
        }
        set
        {
            YandexGame.savesData.MoneyAmount = value;
            YandexGame.SaveProgress();
            _clickCount = value; // устанавливаем новое значение свойства
        }
    }
    
    private bool _coroutineX2CLicks = false;
    public int ClickMultiplayer = 1;
    
    [SerializeField]public int level = 1;
    [SerializeField]public long experience;
    [SerializeField]public long experienceToNextLevel;

    [SerializeField] public List<GameObject> ParticleSystems;
    
    //[SerializeField] private List<Animator> _animators;
    private void Start()
    {
        LoadData();
        
        
        SetLevel(level);
       
        UpdateUpgradeClickUI();
        _progressUI.RefreshAllUI();
    }

    private void LoadData()
    {
        ClicksCount = YandexGame.savesData.MoneyAmount;
        ClickMultiplayer = YandexGame.savesData.ClickMultiplayer;
        level = YandexGame.savesData.Level;
        experience = YandexGame.savesData.Experience;
        experienceToNextLevel = YandexGame.savesData.ExperienceToNextLevel;
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
            //YandexGame.RewVideoShow(1);
            YGRewardedVideoManager.OpenRewardAd(1);
            //StartCoroutine(TimerX2Coroutine());
        }
           
    }

    public void EndRewardStartTimerX2Coroutine()
    {
        StartCoroutine(TimerX2Coroutine());
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
        YandexGame.SaveProgress();
    }

    private void UpdateUpgradeClickUI()
    {
        if(upgradeClickCostUI != null)
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
        // try
        // {
        //     YandexGame.RewVideoShow(2);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        // }
        YGRewardedVideoManager.OpenRewardAd(2);
        
        // ClickMultiplayer++;
        // UpdateUpgradeClickUI();
        // StartCoroutine(TimerUpdateCoroutine());
        // YandexGame.savesData.ClickMultiplayer = ClickMultiplayer;
        // YandexGame.SaveProgress();
    }

    public void EndRewardUpgradeClick()
    {
        ClickMultiplayer++;
        UpdateUpgradeClickUI();
        StartCoroutine(TimerUpdateCoroutine());
        YandexGame.savesData.ClickMultiplayer = ClickMultiplayer;
        YandexGame.SaveProgress();
    }
    
    
    IEnumerator TimerUpdateCoroutine()
    {
        _buttonUpdate.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonUpdate.interactable = true;
    }

    public void AddExperience(long experienceToAdd)
    {
        experience += experienceToAdd;
        
        if (experience >= experienceToNextLevel)
        {
            experience = 0;
            SetLevel(level + 1);
        }
        YandexGame.savesData.Experience = experience;
        YandexGame.SaveProgress();
    }

    private void SetLevel(int value)
    {
        level = value;
        experienceToNextLevel = (int)(50f * (Mathf.Pow(level + 1, 2) - (5 * (level + 1)) + 8));
        UpdateVisual();
        
        YandexGame.savesData.Level = level;
        YandexGame.savesData.ExperienceToNextLevel = experienceToNextLevel;
        GameController.SetLeaderboard(level);
        YandexGame.SaveProgress();
        
        _progressUI.RefreshAllUI();
    }

    public void UpdateVisual()
    {
        Debug.Log(level.ToString("0") + "\nto next lvl: " + experienceToNextLevel + "\ncurrent exp: " + experience);
        //lvlUI.text = _level.ToString();
    }
}