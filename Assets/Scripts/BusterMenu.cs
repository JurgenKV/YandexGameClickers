using System;
using TMPro;
using UnityEngine;
using YG;

public class BusterMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text countHealth;
    [SerializeField] private TMP_Text countMoney;
    [SerializeField] private TMP_Text countScore;

    [SerializeField] private ProgressUI _progressUI;
    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        countHealth.text = YandexGame.savesData.healthBuster.ToString();
        countMoney.text = YandexGame.savesData.moneyBuster.ToString();
        countScore.text = YandexGame.savesData.scoreBuster.ToString();
        try
        {
            FindObjectOfType<ProgressUI>().RefreshMoneyUI();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void ButtonBuyHealth(int cost)
    {
        if(_progressUI.Money < cost)
            return;
        _progressUI.Money -= cost;
        YandexGame.savesData.healthBuster++;
        YandexGame.SaveProgress();
        RefreshUI();
    }
    
    public void ButtonBuyMoney(int cost)
    {
        if(_progressUI.Money < cost)
            return;
        _progressUI.Money -= cost;
        YandexGame.savesData.moneyBuster++;
        YandexGame.SaveProgress();
        RefreshUI();
    }
    
    public void ButtonBuyScore(int cost)
    {
        if(_progressUI.Money < cost)
            return;
        _progressUI.Money -= cost;
        YandexGame.savesData.scoreBuster++;
        YandexGame.SaveProgress();
        RefreshUI();
    }
    
    public void ADSButtonBuyHealthEND()
    {
        YandexGame.savesData.healthBuster++;
        YandexGame.SaveProgress();
        RefreshUI();
    }
    
    public void ADSButtonBuyMoneyEND()
    {
        YandexGame.savesData.moneyBuster++;
        YandexGame.SaveProgress();
        RefreshUI();
    }
    
    public void ADSButtonBuyScoreEND()
    {
        YandexGame.savesData.scoreBuster++;
        YandexGame.SaveProgress();
        RefreshUI();
    }
    
    public void ADSButtonBuyHealthSTART()
    {
        YGRewardedVideoManager.OpenRewardAd(4);
    }
    
    public void ADSButtonBuyMoneySTART()
    {
        YGRewardedVideoManager.OpenRewardAd(5);

    }
    
    public void ADSButtonBuyScoreSTART()
    {
        YGRewardedVideoManager.OpenRewardAd(6);

    }
}