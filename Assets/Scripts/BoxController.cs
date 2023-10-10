﻿
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

enum BoxType
{
    Default,
    Health,
    Damage
}
public class BoxController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private Animator _boxAnimator;
    [SerializeField] private Image _image;
    [SerializeField] private ParticleSystem _particleSystem;

    private BoxType _boxType;
    private bool _isInteractable = false;
    private static readonly int SpeedMultiplayer = Animator.StringToHash("SpeedMultiplayer");
    private static readonly int ClickButton = Animator.StringToHash("ClickButton");
    private static readonly int Start1 = Animator.StringToHash("Start");

    private void Start()
    {
        
    }

    private void Update()
    {
        _boxAnimator.SetFloat(SpeedMultiplayer, _gameController.SpeedMultiplayer);
    }

    public void Click()
    {
        if(!_gameController.IsGameStarted || _gameController.IsGameOver || _gameController.IsOnPause || !_isInteractable)
            return;
        
        _isInteractable = false;
        switch (_boxType)
        {
            case BoxType.Default:
                _gameController.ScoreAmount++;
                _gameController.MoneyAmount += 1 + (int)(1 * _gameController.SpeedMultiplayer);
                _particleSystem.Play();
                break;
            
            case BoxType.Health:
                _gameController.HealthBar.Regenerate();
                break;
            
            case BoxType.Damage:
                _gameController.HealthBar.Damage();
                break;
        }
        
        _boxAnimator.SetBool(ClickButton, true);
    }

    private void WithoutClickEvent()
    {
        if(_boxType != BoxType.Default)
            return;
        
        _gameController.HealthBar.Damage();
    }

    public void GenerateBoxEvent()
    {
        GenerateBoxType();
    }

    private void SetClickButtonFalse()
    {
        _boxAnimator.SetBool(ClickButton, false);
    }
    
    private void SetStartFalse()
    {
        _boxAnimator.SetBool(Start1, false);
    }


    private void GenerateBoxType()
    {
        int rand = Random.Range(1, 101);

        switch (rand)
        {
            case < 50:
                _boxType = BoxType.Default;
                _image.sprite = _gameController.SpritesToSpawn[Random.Range(0,_gameController.SpritesToSpawn.Count)];
                break;
            case >= 50 and <+ 70 :
                _boxType = BoxType.Health;
                _image.sprite = _gameController.HealthSprite;
                break;
            case > 70:
                _boxType = BoxType.Damage;
                _image.sprite = _gameController.DamageSprite;
                break;
        }

        StartCoroutine(StartAnimCor());
    }

    private IEnumerator StartAnimCor()
    {
        yield return new WaitForSeconds(3/_gameController.SpeedMultiplayer);
        _isInteractable = true;
        _boxAnimator.SetBool(Start1, true);
        
    }
}