using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;


public class ChangeBG : MonoBehaviour
{
    [SerializeField] private GameObject bgParent;
    [SerializeField] private ClickerScore _clickerScore;
    [SerializeField] private Button _button;
    [SerializeField] private int _cost = 250;

    [SerializeField] private List<Image> _images = new List<Image>();
    [SerializeField] private Image _defaultImage;

    private int _currentBgIndex = 0;
    private void Start()
    {
        foreach (Image image in _images)
        {
            image.enabled = false;
            image.gameObject.SetActive(false);
        }
        _defaultImage.enabled = true;

        LoadBg(YandexGame.savesData.BgNum);
    }

    public void OnBgChange()
    {
        if (_clickerScore.ClicksCount < _cost)
            return;
        _clickerScore.ClicksCount -= _cost;
        foreach (Image image in _images)
        {
            image.enabled = false;
            image.gameObject.SetActive(false);
        }
        _defaultImage.enabled = false;
        int index;
        do
        { 
            index = Random.Range(0, _images.Count);
        } while (_currentBgIndex == index);
        _currentBgIndex = index;
        _images[_currentBgIndex].enabled = true;
        _images[_currentBgIndex].gameObject.SetActive(true);
        YandexGame.savesData.BgNum = _currentBgIndex;
        YandexGame.SaveProgress();
    }

    public void LoadBg(int bgNum)
    {
        Debug.Log(bgNum);
        if (bgNum == -1)
        {
            foreach (Image image in _images)
            {
                image.enabled = false;
                image.gameObject.SetActive(false);
            }
            _defaultImage.enabled = true;
        }
        else
        {
            foreach (Image image in _images)
            {
                image.enabled = false;
                image.gameObject.SetActive(false);
            }
            _defaultImage.enabled = false;
            _currentBgIndex = bgNum;
            _images[bgNum].enabled = true;
            _images[bgNum].gameObject.SetActive(true);
        }
    }
    
    public void ADSBgClick()
    {
        foreach (Image image in _images)
        {
            image.enabled = false;
            image.gameObject.SetActive(false);
        }
        _defaultImage.enabled = false;
        int index;
        do
        { 
            index = Random.Range(0, _images.Count);
        } while (_currentBgIndex == index);

        _currentBgIndex = index;
        _images[_currentBgIndex].enabled = true;
        _images[_currentBgIndex].gameObject.SetActive(true);
        StartCoroutine(TimerBgCoroutine());
        YandexGame.savesData.BgNum = _currentBgIndex;
        YandexGame.SaveProgress();
        
        try
        {
            YandexGame.RewVideoShow(3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }
    IEnumerator TimerBgCoroutine()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(60);
        _button.interactable = true;
    }

    public void ChangeGirl(int cost)
    {
        if (_clickerScore.ClicksCount < cost)
            return;
        _clickerScore.ClicksCount -= cost;

        YandexGame.savesData.IsGirlUndressed = false;
        YandexGame.savesData.GirlNumber = UnityEngine.Random.Range(0, 5);
        YandexGame.SaveProgress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}