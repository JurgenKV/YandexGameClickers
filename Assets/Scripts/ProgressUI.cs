using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyUI;
    [SerializeField] private TMP_Text _bestScoreUI;

    public long Money
    {
        get => _money;

        set
        {
            _money = value;
            YandexGame.savesData.MoneyAmount = _money;
            YandexGame.SaveProgress();
            RefreshMoneyUI();
        }
    }
    private long _money;

    public long BestScore
    {
        get => _bestScore;

        set
        {
            if(value <= _bestScore)
                return;
            
            _bestScore = value;
            YandexGame.savesData.BestScore = _bestScore;
            YandexGame.SaveProgress();
            RefreshBestScoreUI();
        }
    }
    private long _bestScore;
    
    private void Start()
    {
        Money = YandexGame.savesData.MoneyAmount;
        BestScore = YandexGame.savesData.BestScore;
        RefreshAllUI();
    }

    public void RefreshAllUI()
    {
        RefreshMoneyUI();
        RefreshBestScoreUI();
    }
    
    // private void Update()
    // {
    //     RefreshMoneyUI();
    // }

    public void RefreshMoneyUI()
    {
        if(_bestScoreUI != null)
            _moneyUI.text = Money.ToString() + "$";
    }

    private void RefreshBestScoreUI()
    {
        if(_bestScoreUI != null)
            _bestScoreUI.text = BestScore.ToString();
    }

    public static float GetPercent(long a, long b)
    {
        if (a == 0) 
            return 0;
        
        return ( ((float)a / (float)b) * (float)100);
    }

}