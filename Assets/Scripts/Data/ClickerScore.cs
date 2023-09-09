using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using YG;
using Button = UnityEngine.UI.Button;


public class ClickerScore : MonoBehaviour
{
    [SerializeField] private TMP_Text clicksUI;
    [SerializeField] private Button _buttonX2;
    [SerializeField] private Button _buttonUpdate;

    public long ClicksCount = 0;
    private bool _coroutineX2CLicks = false;
    public int ClickMultiplayer = 1;

    [SerializeField] public List<GameObject> ParticleSystems;
    //[SerializeField] private List<Animator> _animators;

    private void Update()
    {
        clicksUI.text = ClicksCount.ToString() + "$";
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

    public void UpgradeClick(int cost)
    {
        if (ClicksCount < cost)
            return;
        
        ClicksCount -= cost;
        
        ClickMultiplayer++;
        YandexGame.savesData.ScoreMultiplayer = ClickMultiplayer;
        YandexGame.SaveProgress();
    }
    
    public void ADSUpgradeClick()
    {
        ClickMultiplayer++;
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
        YandexGame.SaveProgress();
    }
    IEnumerator TimerUpdateCoroutine()
    {
        _buttonUpdate.interactable = false;
        yield return new WaitForSeconds(60);
        _buttonUpdate.interactable = true;
    }
}