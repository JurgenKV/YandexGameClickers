using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{
    [SerializeField] private ItemsSO _itemsSo;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _scoreIfLose;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private Button _slowButton;
    
    
    [SerializeField] public List<GameObject> wellClickParticles;
    [SerializeField] public List<GameObject> healthParticles;
    [SerializeField] public List<GameObject> damageParticles;
    
    [SerializeField] public List<AudioSource> AudioSources;
    
    public bool IsGameStarted = false;
    public bool IsOnPause = false;
    public bool IsOnRewardPause = false;
    public bool IsGameOver = false;
    public float SpeedMultiplayer = 1;

    private bool _slowCounterCoroutineActive = false;
    
    public HealthBar HealthBar;

    public List<Sprite> SpritesToSpawn;
    public Sprite HealthSprite;
    public List<Sprite> DamageSprites;

    private int _scoreAmount;
    
    public bool HpBuster = false;
    public bool MoneyBuster = false;
    public bool ScoreBuster = false;

    public int ScoreAmount
    {
        get => _scoreAmount;
        set
        {
            _scoreAmount = value;
            _score.text = _scoreAmount.ToString();
            _scoreIfLose.text = _scoreAmount.ToString();
            
            try
            {
                if (_scoreAmount <= YandexGame.savesData.BestScore) 
                    return;
                
                YandexGame.savesData.BestScore = _scoreAmount;
                YandexGame.SaveProgress();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public int MoneyAmount
    {
        get => _moneyAmount;
        set
        {
            try
            {
                YandexGame.savesData.MoneyAmount += value - _moneyAmount;
                YandexGame.SaveProgress();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            _moneyAmount = value;
            _money.text = _moneyAmount.ToString() + "$";
        }
    }

    private int _moneyAmount = 0;

    private void Awake()
    {
        CheckBusters();
    }

    private void Start()
    {
        FillSpriteList();
        IsGameStarted = true;
        InvokeRepeating(nameof(IncreaseSpeedMultiplayer), 1,9);
    }

    private void CheckBusters()
    {
        if (YandexGame.savesData.healthBuster > 0)
        {
            HpBuster = true;
            YandexGame.savesData.healthBuster -= 1;
            YandexGame.SaveProgress();
        }
        
        if (YandexGame.savesData.moneyBuster > 0)
        {
            MoneyBuster = true;
            YandexGame.savesData.moneyBuster -= 1;
            YandexGame.SaveProgress();
        }
        
        if (YandexGame.savesData.scoreBuster > 0)
        {
            ScoreBuster = true;
            YandexGame.savesData.scoreBuster -= 1;
            YandexGame.SaveProgress();
        }
    }

    private void Update()
    {
        if (HealthBar.TempHealth == 0 & !IsGameOver)
            GameOver();
        
    }

    private void IncreaseSpeedMultiplayer()
    {
        if (!IsGameStarted || IsGameOver || IsOnPause || IsOnRewardPause || _slowCounterCoroutineActive)
            return;
        
        SpeedMultiplayer += 0.1f;
    }

    private void GameOver()
    {
        IsGameOver = true;
        SetLeaderboard((int)YandexGame.savesData.BestScore);
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

    public void StartRewardSlowTime()
    {
        YGRewardedVideoManager.OpenRewardAd(2);
        IsOnRewardPause = true;
    }
    
    public void StartRewardHealth()
    {
        YGRewardedVideoManager.OpenRewardAd(3);
        IsOnRewardPause = true;
    }
    
    public void EndRewardSlowTime()
    {
        IsOnRewardPause = false;
        if(!_slowCounterCoroutineActive)
            StartCoroutine(SlowCounterCoroutine());
    }
    
    public void EndRewardHealth()
    {
        HealthBar.Regenerate();
        IsOnRewardPause = false;
        IsGameOver = false;
        _gameOverUI.SetActive(false);
    }

    IEnumerator SlowCounterCoroutine()
    {
        _slowButton.interactable = false;
        _slowCounterCoroutineActive = true;
        float tempMultiplayer = SpeedMultiplayer;
        SpeedMultiplayer /= 2;
        
        for (int i = 0; i < 30; i++)
        {
            if (IsOnPause)
                yield return new WaitWhile(() => IsOnPause);
            
            yield return new WaitForSeconds(1);
        }

        SpeedMultiplayer = tempMultiplayer;
        _slowCounterCoroutineActive = false;
        _slowButton.interactable = true;
    }

    public static void SetLeaderboard(int num)
    {
        try
        {
            YandexGame.NewLeaderboardScores("BestScorePlayerPusheenCatch", num);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
}