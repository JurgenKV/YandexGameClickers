using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class CatchController : MonoBehaviour
{
    [SerializeField] public HealthBar HealthBar;

    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private TMP_Text _moneyTXT;
    private long _clickCount = 0;

    [SerializeField] private GameObject item;
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject damage;

    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;

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
            _moneyTXT.text = _clickCount.ToString() + "$";
        }
    }
    
    public int ClickMultiplayer = 1;
    public bool IsGameOver = false;
    public float repeatCount = 1;
    private void Start()
    {
        LoadData();
        _moneyTXT.text = _clickCount.ToString() + "$";
        
        Invoke(nameof(GenerateItem),1);
        Invoke(nameof(GenerateItem),3);
    }

    private void Update()
    {
        if (HealthBar.TempHealth == 0 & !IsGameOver)
            GameOver();
    }
    
    private void LoadData()
    {
        ClicksCount = YandexGame.savesData.MoneyAmount;
        ClickMultiplayer = YandexGame.savesData.ClickMultiplayer;
    }

    private void GameOver()
    {
        IsGameOver = true;
        _gameOverMenu.SetActive(true);
    }

    private void GenerateItem()
    {
        if(IsGameOver)
            return;
        
        int num = Random.Range(0, 100);
        switch (num)
        {
            case > 0 and <= 50:
                Instantiate(item,
                    new Vector3(Random.Range(point1.transform.position.x, point2.transform.position.x),
                        point1.transform.position.y, 1), Quaternion.identity);
                break;
            
            case > 50 and <= 70:
                Instantiate(health,
                    new Vector3(Random.Range(point1.transform.position.x, point2.transform.position.x),
                        point1.transform.position.y, 1), Quaternion.identity);
                break;
            
            case > 70:
                Instantiate(damage,
                    new Vector3(Random.Range(point1.transform.position.x, point2.transform.position.x),
                        point1.transform.position.y, 1), Quaternion.identity);
                break;
        }
        repeatCount++;
        Invoke(nameof(GenerateItem),Random.Range(1,5));
    }
}