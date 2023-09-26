using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class CollectionButton : MonoBehaviour
{
    [SerializeField] private ClickerScore _clickerScore;
    [SerializeField] private Button _button;
    [SerializeField] private int _cost = 250;
    [SerializeField] private int maxCollectionCount;

    [SerializeField] private Image helpImage;
    
    private void Start()
    {
        helpImage.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (_clickerScore.ClicksCount < _cost)
            return;
        
        _clickerScore.ClicksCount -= _cost;
        
        int index = Random.Range(0, maxCollectionCount + 1);
        
        YandexGame.savesData.PusheenNums.Add(index);
        StartCoroutine(TimerCoroutine());
        
        YandexGame.SaveProgress();
    }

    IEnumerator TimerCoroutine()
    {
        helpImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(15);
        helpImage.gameObject.SetActive(false);
    }
}