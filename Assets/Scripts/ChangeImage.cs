using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

enum SpriteType
{
    Background,
    Object
}

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private ClickerScore _clickerScore;
    [SerializeField] private Button _button;
    [SerializeField] private int _cost = 250;

    [SerializeField] private List<Sprite> _images = new List<Sprite>();
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Image _Image;
    [SerializeField] private SpriteType _spriteType;
    
    private int _currentBgIndex = 0;
    private void Start()
    {
        _Image.sprite = _defaultSprite;
        
        if(_spriteType == SpriteType.Background)
            LoadImage(YandexGame.savesData.BgNum);
        
        // if(_spriteType == SpriteType.Object)
        //     LoadImage(YandexGame.savesData.ObjectImageNum);
    }

    public void OnImageChange()
    {
        if (_clickerScore.ClicksCount < _cost)
            return;
        _clickerScore.ClicksCount -= _cost;
        
        int index;
        do
        { 
            index = Random.Range(0, _images.Count);
        } while (_currentBgIndex == index);
        _currentBgIndex = index;
        _Image.sprite = _images[_currentBgIndex];
        
        if(_spriteType == SpriteType.Background)
            YandexGame.savesData.BgNum = _currentBgIndex;
        
        // if(_spriteType == SpriteType.Object)
        //     YandexGame.savesData.ObjectImageNum = _currentBgIndex;
        
        YandexGame.SaveProgress();
    }

    public void LoadImage(int bgNum)
    {
        Debug.Log(bgNum);
        if (bgNum == -1)
        {
            _Image.sprite = _defaultSprite;
        }
        else
        {
            _currentBgIndex = bgNum;
            _Image.sprite = _images[bgNum];
        }
    }
    
    public void ADSBgClick()
    {
        // try
        // {
        //     YandexGame.RewVideoShow(3);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        // }
        
        YGRewardedVideoManager.OpenRewardAd(3);
        //
        // int index;
        // do
        // { 
        //     index = Random.Range(0, _images.Count);
        // } while (_currentBgIndex == index);
        //
        // _currentBgIndex = index;
        // _Image.sprite = _images[_currentBgIndex];
        // StartCoroutine(TimerBgCoroutine());
        //
        // if(_spriteType == SpriteType.Background)
        //     YandexGame.savesData.BgNum = _currentBgIndex;
        //
        // YandexGame.SaveProgress();
        //
    }

    public void EndRewardADSBgClick()
    {
        
        int index;
        do
        { 
            index = Random.Range(0, _images.Count);
        } while (_currentBgIndex == index);

        _currentBgIndex = index;
        _Image.sprite = _images[_currentBgIndex];
        StartCoroutine(TimerBgCoroutine());
        
        if(_spriteType == SpriteType.Background)
            YandexGame.savesData.BgNum = _currentBgIndex;

        YandexGame.SaveProgress();

    }
    
    IEnumerator TimerBgCoroutine()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(60);
        _button.interactable = true;
    }
}