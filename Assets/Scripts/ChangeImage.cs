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

    [SerializeField] private List<ColorsCollection> _spritesLists;
    [SerializeField] private Image _Image;
    [SerializeField] private SpriteType _spriteType;

    [SerializeField] private List<GameObject> _shadows = new List<GameObject>();
    private int _currentIndex = 0;
    private void Start()
    {

        // if(_spriteType == SpriteType.Background)
        //     LoadImage(YandexGame.savesData.BgNum);
        
        if(_spriteType == SpriteType.Object)
            LoadImage(YandexGame.savesData.catType);
    }

    public void OnImageChange()
    {
        if (_clickerScore.ClicksCount < _cost)
            return;
        
        _clickerScore.ClicksCount -= _cost;
        
        int index;
        do
        { 
            index = Random.Range(0, _spritesLists.Count);
        } while (_currentIndex == index);
        _currentIndex = index;
        _Image.sprite = GetImageByLevel(_currentIndex);
        
        // if(_spriteType == SpriteType.Background)
        //     YandexGame.savesData.BgNum = _currentBgIndex;
        
        if(_spriteType == SpriteType.Object)
            YandexGame.savesData.catType = _currentIndex;
        
        YandexGame.SaveProgress();
    }

    public void LoadImage(int bgNum)
    {
        Debug.Log(bgNum);
        if (bgNum == -1)
        {
           // _Image.sprite = _defaultSprite;
        }
        else
        {
            _currentIndex = bgNum;
            _Image.sprite = GetImageByLevel(_currentIndex);
        }
    }
    
    public void ADSBgClick()
    {
        YGRewardedVideoManager.OpenRewardAd(3);
    }

    public void EndRewardADSBgClick()
    {
        
        int index;
        do
        { 
            index = Random.Range(0, _spritesLists.Count);
        } while (_currentIndex == index);
        _currentIndex = index;
        _Image.sprite = GetImageByLevel(_currentIndex);
        
        // if(_spriteType == SpriteType.Background)
        //     YandexGame.savesData.BgNum = _currentBgIndex;
        
        if(_spriteType == SpriteType.Object)
            YandexGame.savesData.catType = _currentIndex;

        StartCoroutine(TimerBgCoroutine());
        // if(_spriteType == SpriteType.Background)
        //     YandexGame.savesData.BgNum = _currentBgIndex;

        YandexGame.SaveProgress();

    }
    
    IEnumerator TimerBgCoroutine()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(30);
        _button.interactable = true;
    }

    private Sprite GetImageByLevel(int index)
    {
        foreach (GameObject shadow in _shadows)
        {
            shadow.SetActive(false);
        }

        int ind = _clickerScore.level - 1;
        if(ind < 3 )
            _shadows[0].SetActive(true);

        if (3 <= ind & ind <= 5)
            _shadows[1].SetActive(true);
        
        if(ind > 5)
            _shadows[2].SetActive(true);

        if (_clickerScore.level < 10)
            return _spritesLists[index].Sprites[_clickerScore.level - 1];
        else
            return _spritesLists[index].Sprites.Last();
    }
    
    [Serializable]
    protected class ColorsCollection
    {
        public List<Sprite> Sprites = new List<Sprite>();
    }
}

