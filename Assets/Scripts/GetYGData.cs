using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GetYGData : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Animator _animator;

    [SerializeField] private UnityEngine.UI.Button _ruButton;
    [SerializeField] private UnityEngine.UI.Button _enButton;
    [SerializeField] private UnityEngine.UI.Button _trButton;

    private bool _data = false;
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void GetData()
    {
        Debug.Log(YandexGame.savesData.MoneyAmount + "Score");
        _data = true;
    }

    public void LoadGameEvent()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void RuEngButton()
    {
        DisableAllButtons();
        StartCoroutine(Wait("ru"));
    }

    public void EnEngButton()
    {
        DisableAllButtons();
        StartCoroutine(Wait("en"));
    }
    
    public void TrEngButton()
    {
        DisableAllButtons();
        StartCoroutine(Wait("tr"));
    }

    private IEnumerator Wait(string language = "ru")
    {
        yield return new WaitWhile(() => _data == false);
        YandexGame.savesData.language = language;
        
        if (YandexGame.savesData.SoldItems.Count == 0 && YandexGame.savesData.ActiveItems.Count == 0)
        {
            YandexGame.savesData.SoldItems.Add(0);
            YandexGame.savesData.ActiveItems.Add(0);
            YandexGame.savesData.SoldItems.Add(1);
            YandexGame.savesData.ActiveItems.Add(1);
            YandexGame.savesData.SoldItems.Add(2);
            YandexGame.savesData.ActiveItems.Add(2);
            YandexGame.SaveProgress();
        }
        
        LoadGameEvent();
    }

    private void DisableAllButtons()
    {
        _ruButton.interactable = false;
        _enButton.interactable = false;
        _trButton.interactable = false;
    }

}