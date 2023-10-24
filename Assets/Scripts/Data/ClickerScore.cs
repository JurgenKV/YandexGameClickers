using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Button _buttonAuto;
    [SerializeField] private Button _buttonUpdate;
    
    [Header("Upgrade Exp Multiplayer")]
    [SerializeField] private TMP_Text upgradeExpCostUI;
    [SerializeField] private Button _buttonUpgaradeExp;

    [SerializeField] private ChangeImage _changeImage;
    
    [SerializeField] private List<AudioSource> _sources = new List<AudioSource>();
    [Header("Sim Content")]
    [SerializeField] private List<GameObject> _simsContent;
    [SerializeField] private Button _clearSimButton;
    
    [Header("End Sim Content")]
    public bool IsAutoClickActive = false;
    
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
    [SerializeField]public long experienceMultiplayer = 1;
    [SerializeField]public long experience;
    [SerializeField]public long experienceToNextLevel;

    [SerializeField] public List<GameObject> ParticleSystems;
    private static readonly int Active = Animator.StringToHash("Active");

    //[SerializeField] private List<Animator> _animators;
    private void Start()
    {
        LoadData();
        
        
        SetLevel(level);
       
        UpdateUpgradeClickUI();
        UpdateUpgradeExpUI();
        _progressUI.RefreshAllUI();
        
        InvokeRepeating(nameof(AutoExp), 1,1);
        Invoke(nameof(GenerateContent), 60);
    }

    private void LoadData()
    {
        ClicksCount = YandexGame.savesData.MoneyAmount;
        ClickMultiplayer = YandexGame.savesData.ClickMultiplayer;
        level = YandexGame.savesData.Level;
        experience = YandexGame.savesData.Experience;
        experienceToNextLevel = YandexGame.savesData.ExperienceToNextLevel;
        experience = YandexGame.savesData.Experience;
        experienceMultiplayer = YandexGame.savesData.ExperienceMultiplayer;

        if (YandexGame.savesData.ExperienceMultiplayer == 0)
        {
            experienceMultiplayer = YandexGame.savesData.ExperienceMultiplayer = 1;
            YandexGame.SaveProgress();
        }

        LoadContent();
    }

    public void Click()
    {
        if(!gameObject.activeSelf)
            return;

        if (_coroutineX2CLicks)
        {
            ClicksCount += 1 * ClickMultiplayer * 2;
            //AddExperience(1 * ClickMultiplayer * 2);
        }
        else
        {
            ClicksCount += 1 * ClickMultiplayer;
            //AddExperience(1 * ClickMultiplayer);
        }
        Debug.Log("Click");
        YandexGame.savesData.MoneyAmount = ClicksCount;
        YandexGame.SaveProgress();
        _progressUI.RefreshAllUI();
    }

    public void ButtonClearContent()
    {
        if (_simsContent.TrueForAll(i => !i.activeSelf))
            return;
        
        if(ClicksCount < 50)
            return;
        
        ClicksCount -= 50;
        
        AddExperience((long) (experienceMultiplayer * 3f) + 5);

        int index = _simsContent.FindIndex(i => i.activeSelf);
        _simsContent[index].GetComponent<Animator>().SetBool(Active, false);
        _simsContent[index].SetActive(false);
        YandexGame.savesData.ContentNums.Remove(index);
        YandexGame.SaveProgress();
        CheckButtonClearState();
    }

    private void GenerateContent()
    {
        if (_simsContent.TrueForAll(i => i.activeSelf))
        {
            Invoke(nameof(GenerateContent), Random.Range(60, 180));
            return;
        }

        int index = -1;
            
        while (true)
        {
            index = Random.Range(0, _simsContent.Count);
            
            if(!_simsContent[index].activeSelf)
                break;
        }
        _simsContent[index].SetActive(true);
        _simsContent[index].GetComponent<Animator>().SetBool(Active, true);

        YandexGame.savesData.ContentNums.Add(index);
        YandexGame.SaveProgress();
        CheckButtonClearState();
        Invoke(nameof(GenerateContent), Random.Range(60, 180));
    }

    private void LoadContent()
    {
        if (YandexGame.savesData.ContentNums == null)
        {
            YandexGame.savesData.ContentNums = new List<int>();
            YandexGame.SaveProgress();
            return;
        }
            
        
        for (int i = 0; i < _simsContent.Count; i++)
        {
            if (YandexGame.savesData.ContentNums.Contains(i))
            {
                _simsContent[i].SetActive(true);
                _simsContent[i].GetComponent<Animator>().SetBool(Active, true);
            }
        }

        CheckButtonClearState();
    }

    private void CheckButtonClearState()
    {
        if(_simsContent.TrueForAll(i=> !i.activeSelf))
            _clearSimButton.interactable = false;
        else
            _clearSimButton.interactable = true;
    }
    private void AutoExp()
    {
        AddExperience(experienceMultiplayer);
        _progressUI.RefreshAllUI();
    }

    public void ConvertMoneyToExp(int cost)
    {
        if(ClicksCount < cost)
            return;
        ClicksCount -= cost;

        AddExperience((long) (ClickMultiplayer * 1.5f) + 5);
        
        if (YandexGame.savesData.IsSoundEnabled)
        {
            if (_sources.All(i => !i.isPlaying))
            {
                _sources[Random.Range(0, _sources.Count)].Play();
            }
        }
        _progressUI.RefreshAllUI();
        YandexGame.savesData.MoneyAmount = ClicksCount;
        YandexGame.SaveProgress();
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
    
    public void EndRewardAutoClickCoroutine()
    {
        StartCoroutine(TimerAutoClickCoroutine());
    }

    IEnumerator TimerX2Coroutine()
    {
        _coroutineX2CLicks = true;
        _buttonX2.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonX2.interactable = true;
        _coroutineX2CLicks = false;
    }
    
    IEnumerator TimerAutoClickCoroutine()
    {
        IsAutoClickActive = true;
        _buttonAuto.interactable = false;
        yield return new WaitForSeconds(90);
        _buttonAuto.interactable = true;
        IsAutoClickActive = false;
    }

    public void UpgradeClick()
    {
         if (ClicksCount < GetUpgradeCostClick())
             return;
        
         ClicksCount -= GetUpgradeCostClick();
        
        ClickMultiplayer++;
        UpdateUpgradeClickUI();
        YandexGame.savesData.ClickMultiplayer = ClickMultiplayer;
        YandexGame.SaveProgress();
    }
    
    public void UpgradeExpMultiplayer()
    {
        if (ClicksCount < GetUpgradeCostExp())
            return;
        
        ClicksCount -= GetUpgradeCostExp();
        
        experienceMultiplayer++;
        UpdateUpgradeExpUI();
        YandexGame.savesData.ExperienceMultiplayer = experienceMultiplayer;
        YandexGame.SaveProgress();
    }

    private void UpdateUpgradeClickUI()
    {
        if(upgradeClickCostUI != null)
            upgradeClickCostUI.text = GetUpgradeCostClick().ToString() + "$";
    }
    
    private void UpdateUpgradeExpUI()
    {
        if(upgradeExpCostUI != null)
            upgradeExpCostUI.text = GetUpgradeCostExp().ToString() + "$";
    }

    private long GetUpgradeCostClick()
    {
        //x * ((level+1) ^ y) - (x * level):
        double cost = ((constX * Math.Pow((ClickMultiplayer+1), degreeY)) - (constX * (ClickMultiplayer+1)));
        Debug.Log("cost = " + cost);
        
        
        return (long)cost;
    }
    
    private long GetUpgradeCostExp()
    {
        //x * ((level+1) ^ y) - (x * level):
        double cost = ((constX * Math.Pow((experienceMultiplayer+1.2), degreeY)) - (constX * (experienceMultiplayer+1)));
        Debug.Log("cost = " + cost);
        
        
        return (long)cost;
    }

    public void ADSUpgradeClick()
    {
        YGRewardedVideoManager.OpenRewardAd(2);
    }
    
    public void ADSUpgradeExp()
    {
        YGRewardedVideoManager.OpenRewardAd(6);
    }
    
    public void ADSAutoClick()
    {
        YGRewardedVideoManager.OpenRewardAd(5);
    }

    public void EndRewardUpgradeClick()
    {
        ClickMultiplayer++;
        UpdateUpgradeClickUI();
        StartCoroutine(TimerUpdateCoroutine());
        YandexGame.savesData.ClickMultiplayer = ClickMultiplayer;
        YandexGame.SaveProgress();
    }
    
    public void EndRewardUpgradeMultiplayer()
    {
        experienceMultiplayer++;
        UpdateUpgradeExpUI();
        StartCoroutine(TimerUpdateExpCoroutine());
        YandexGame.savesData.ExperienceMultiplayer = experienceMultiplayer;
        YandexGame.SaveProgress();
    }
    
    
    IEnumerator TimerUpdateCoroutine()
    {
        _buttonUpdate.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonUpdate.interactable = true;
    }
    
    IEnumerator TimerUpdateExpCoroutine()
    {
        _buttonUpgaradeExp.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonUpgaradeExp.interactable = true;
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
        experienceToNextLevel = (int)(500 * (Mathf.Pow(level + 1, 2) - (5 * (level + 1)) + 8));
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
        _changeImage.LoadImage(YandexGame.savesData.catType);
    }
}