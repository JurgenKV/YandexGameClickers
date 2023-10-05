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
        _ruButton.interactable = false;
        _enButton.interactable = false;
        StartCoroutine(WaitRu());
    }

    public void EnEngButton()
    {
        _ruButton.interactable = false;
        _enButton.interactable = false;
        StartCoroutine(WaitEn());
    }

    private IEnumerator WaitRu()
    {
        yield return new WaitWhile(() => _data == false);
        YandexGame.savesData.language = "ru";
        LoadGameEvent();
    }
    
    private IEnumerator WaitEn()
    {
        yield return new WaitWhile(() => _data == false);
        YandexGame.savesData.language = "en";
        LoadGameEvent();
    }
}