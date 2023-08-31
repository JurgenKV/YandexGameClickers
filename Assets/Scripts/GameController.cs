using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionsList _questionsList;
    [SerializeField] private QuestionUI _questionUis;
    [SerializeField] private GameObject _startGamePG;
    [SerializeField] private GameObject _endGamePG;
    [SerializeField] private TMP_Text _questionCount;
    [SerializeField] private ShowWinner _showWinner;

    [SerializeField] public List<QuestionItem> _questionItems = new List<QuestionItem>();
    private void Start()
    {
        _questionsList.NewGame();
        _questionItems.Clear();
        _startGamePG.SetActive(true);
        _endGamePG.SetActive(false);
        foreach (var questionItem in GetLocalizationDataList())
        {
            _questionItems.Add(questionItem);
        }
        YandexGame.FullscreenShow();
        Shuffle(_questionItems);
        CreateNewUI();
    }

    private List<QuestionItem> GetLocalizationDataList()
    {
        Debug.Log((YandexGame.EnvironmentData.language.ToString()));
        if (YandexGame.EnvironmentData.language.Equals("en"))
        {
            return _questionsList.ENQuestionItems;
        }
        else if (YandexGame.EnvironmentData.language.Equals("tr"))
        {
            return _questionsList.TRQuestionItems;
        }
        
        return _questionsList.QuestionItems;
    }

    private void CreateNewUI()
    {
        _questionUis.CreateUI(_questionItems.First());
        ChangeCountOfQuestions();
    }

    public void HandleButtonVariantFirst(QuestionItem questionItem)
    {
        _questionsList.Persons.First(i => i.Id.Equals(questionItem.PersonIdFirst)).Score++;
        _questionItems.Remove(questionItem);
        
        if (_questionItems.Count == 0)
        {
            GameOver();
            return;
        }
        
        CreateNewUI();
    }
    
    public void HandleButtonVariantSecond(QuestionItem questionItem)
    {
        _questionsList.Persons.First(i => i.Id.Equals(questionItem.PersonIdSecond)).Score++;
        _questionItems.Remove(questionItem);
        
        if (_questionItems.Count == 0)
        {
            GameOver();
            return;
        }
        
        CreateNewUI();
    }

    private void GameOver()
    {
        Debug.Log("GameCompleted" + GetIdWinGirl());
        _startGamePG.SetActive(false);
        _endGamePG.SetActive(true);
        _showWinner.SetWinner();
    }

    private int GetIdWinGirl()
    {
        return _questionsList.Persons.Max(i => i.Score);
    }

    private static void Shuffle<T>(IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    private void ChangeCountOfQuestions()
    {
        _questionCount.text = $"{_questionsList.QuestionItems.Count - _questionItems.Count + 1}/{_questionsList.QuestionItems.Count}";
    }
}