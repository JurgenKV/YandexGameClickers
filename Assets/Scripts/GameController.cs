using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;

public class GameController : MonoBehaviour
{
    [SerializeField] private ItemsSO _itemsSo;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _scoreIfLose;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private GameObject _gameOverUI;
    public bool IsGameStarted = false;
    public bool IsOnPause = false;
    public bool IsGameOver = false;
    public float SpeedMultiplayer = 1;
    
    public HealthBar HealthBar;

    public List<Sprite> SpritesToSpawn;
    public Sprite HealthSprite;
    public Sprite DamageSprite;

    private int _scoreAmount;

    public int ScoreAmount
    {
        get => _scoreAmount;
        set
        {
            _scoreAmount = value;
            _score.text = _scoreAmount.ToString();
            _scoreIfLose.text = _scoreAmount.ToString();
        }
    }

    public int MoneyAmount
    {
        get => _moneyAmount;
        set
        {
            _moneyAmount = value;
            _money.text = _moneyAmount.ToString() + "$";
        }
    }

    private int _moneyAmount;
    

    private void Start()
    {
        FillSpriteList();
        
        IsGameStarted = true;
    }

    private void Update()
    {
        if (HealthBar.TempHealth == 0 & !IsGameOver)
            GameOver();
    }

    private void GameOver()
    {
        IsGameOver = true;
        IsOnPause = true;
        _gameOverUI.SetActive(true);
    }

    private void FillSpriteList()
    {
        foreach (var item in _itemsSo.Items)
        {
            if(YandexGame.savesData.ActiveItems.Contains(item.Index))
                SpritesToSpawn.Add(item.Sprite);
        }
        
        if(SpritesToSpawn.Count == 0)
            SpritesToSpawn.Add(_itemsSo.Items.First(i=> i.Index == 16).Sprite);
    }
    
    public static void SetLeaderboard(int num)
    {
        try
        {
            YandexGame.NewLeaderboardScores("BestLevelPlayerPusheenClicker", num);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
}