
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
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
    [SerializeField] private GameObject _spawnParticlesPosition;

    private BoxType _boxType;
    private bool _isInteractable = false;
    private static readonly int SpeedMultiplayer = Animator.StringToHash("SpeedMultiplayer");
    private static readonly int ClickButton = Animator.StringToHash("ClickButton");
    private static readonly int Start1 = Animator.StringToHash("Start");
    

    private void Update()
    {
        CheckGameState();
    }

    private void CheckGameState()
    {
        if (!_gameController.IsGameStarted || _gameController.IsGameOver || _gameController.IsOnPause || _gameController.IsOnRewardPause)
        {
            _boxAnimator.SetFloat(SpeedMultiplayer, 0);
        }
        else
        {
            _boxAnimator.SetFloat(SpeedMultiplayer, _gameController.SpeedMultiplayer);
        }
    }

    public void Click()
    {
        if(!_gameController.IsGameStarted || _gameController.IsGameOver || _gameController.IsOnPause || !_isInteractable)
            return;
        
        if(YandexGame.savesData.IsSoundEnabled)
            _gameController.AudioSources[Random.Range(0, _gameController.AudioSources.Count)].Play();

        _isInteractable = false;
        switch (_boxType)
        {
            case BoxType.Default:
                
                if(!_gameController.MoneyBuster)
                    _gameController.MoneyAmount += 1 + (int)(1 * _gameController.SpeedMultiplayer);
                else
                    _gameController.MoneyAmount += ((1 + (int)(1 * _gameController.SpeedMultiplayer))*3);

                if (!_gameController.ScoreBuster)
                    _gameController.ScoreAmount++;
                else
                    _gameController.ScoreAmount += 3;

                Instantiate(_gameController.wellClickParticles[Random.Range(0, _gameController.wellClickParticles.Count)], _spawnParticlesPosition.transform.position, Quaternion.identity);
                break;
            
            case BoxType.Health:
                _gameController.HealthBar.Regenerate();
                Instantiate(_gameController.healthParticles[Random.Range(0, _gameController.healthParticles.Count)], _spawnParticlesPosition.transform.position, Quaternion.identity);
                break;
            
            case BoxType.Damage:
                _gameController.HealthBar.Damage();
                Instantiate(_gameController.damageParticles[Random.Range(0, _gameController.damageParticles.Count)], _spawnParticlesPosition.transform.position, Quaternion.identity);
                break;
        }
        Debug.Log("ClickButton true");
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
        switch (Random.Range(1, 101))
        {
            case < 60:
                _boxType = BoxType.Default;
                _image.sprite = _gameController.SpritesToSpawn[Random.Range(0,_gameController.SpritesToSpawn.Count)];
                break;
            case >= 60 and <= 70 :
                _boxType = BoxType.Health;
                _image.sprite = _gameController.HealthSprite;
                break;
            case > 70:
                _boxType = BoxType.Damage;
                _image.sprite = _gameController.DamageSprites[Random.Range(0,_gameController.DamageSprites.Count)];
                break;
        }
        
        if (Random.Range(0, 2) == 0)
            _image.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        else
            _image.gameObject.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        
        StartCoroutine(StartAnimCor());
    }

    private IEnumerator StartAnimCor()
    {
        yield return new WaitForSeconds(Random.Range(1,5) / _gameController.SpeedMultiplayer);
        _isInteractable = true;
        _boxAnimator.SetBool(Start1, true);
        
    }
}