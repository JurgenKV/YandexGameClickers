using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject _eggObject;
    [SerializeField] private GameObject _animalObject;
    [SerializeField] private GameObject _recolorEgg;
    [SerializeField] private GameObject _recolorEggADS;
    
    [Header("DestrEgg")]
    [SerializeField] private Sprite _nullSprite;
    [SerializeField] private Image _destrEgg;
    [SerializeField] private List<Sprite> _destrEggs;
    
    [Header("Animal")]
    [SerializeField] private Image _animalSprite;
    [SerializeField] public List<Sprite> _animalsSprites;

    private void Start()
    {
        CheckProgress();
        ShowFullAds();
    }

    public void CheckProgress()
    {
        // YandexGame.savesData.Score = 0;
        // YandexGame.savesData.ScoreMultiplayer = 1;
        // YandexGame.savesData.BgNum = -1;
        // YandexGame.savesData.ObjectImageNum = -1;
        // YandexGame.savesData.ObjectImageSecNum = -1;
        // YandexGame.savesData.IsAnimal = false;
        // YandexGame.SaveProgress();
        
        if (!YandexGame.savesData.IsAnimal)
        {
            _eggObject.SetActive(true);
            _animalObject.SetActive(false);
            _recolorEgg.SetActive(true);
            _recolorEggADS.SetActive(true);
        }
        else
        {
            _eggObject.SetActive(false);
            _animalObject.SetActive(true);
            _recolorEgg.SetActive(false);
            _recolorEggADS.SetActive(false);
            
            if (YandexGame.savesData.ObjectImageSecNum == -1)
            {
                _destrEgg.sprite = _nullSprite;
            }
            else
            {
                _destrEgg.sprite = _destrEggs[YandexGame.savesData.ObjectImageSecNum];
            }
        }
    }

    private void ShowFullAds()
    {
        try
        {
            YandexGame.FullscreenShow();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

}