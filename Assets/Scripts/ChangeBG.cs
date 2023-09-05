using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        _images = bgParent.GetComponentsInChildren<Image>().ToList();
        foreach (Image image in _images)
        {
            image.enabled = false;
        }
        _defaultImage.enabled = true;
    }

    public void OnBgChange()
    {
        if (_clickerScore.ClicksCount < _cost)
            return;
        _clickerScore.ClicksCount -= _cost;
        foreach (Image image in _images)
        {
            image.enabled = false;
        }
        _defaultImage.enabled = false;
        int index;
        do
        { 
            index = Random.Range(0, _images.Count);
        } while (_currentBgIndex == index);
        _currentBgIndex = index;
        _images[_currentBgIndex].enabled = true;
    }
    
    public void ADSBgClick()
    {
        foreach (Image image in _images)
        {
            image.enabled = false;
        }
        _defaultImage.enabled = false;
        int index;
        do
        { 
            index = Random.Range(0, _images.Count);
        } while (_currentBgIndex == index);

        _currentBgIndex = index;
        _images[_currentBgIndex].enabled = true;
        
        StartCoroutine(TimerBgCoroutine());
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
        yield return new WaitForSeconds(80);
        _button.interactable = true;
    }
}