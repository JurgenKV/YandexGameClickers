using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionsList _questionsList;
    [SerializeField] private QuestionUI _questionUis;
    [SerializeField] private GameObject _startGamePG;
    [SerializeField] private GameObject _endGamePG;
    [SerializeField] private TMP_Text _questionCount;
    [SerializeField] private ShowWinner _showWinner;
    [SerializeField] private ClickerScore _clickerScore;
    [SerializeField] private GameObject _startDateButton;
    [SerializeField] private GameObject _clickerObject;
    [SerializeField] private ChangeBG _changeBg;
    [SerializeField] private GameObject _hideBlack;
    [SerializeField] public List<QuestionItem> _questionItems = new List<QuestionItem>();
    [SerializeField] private Button _firstButton;
    [SerializeField] private Button _secoundButton;
    private bool _isGameOver = false;
    private void Start()
    {
        if (!YandexGame.savesData.IsTestCompleted)
        {
            _questionsList.NewGame();
            _questionItems.Clear();
            _startGamePG.SetActive(true);
            //_endGamePG.SetActive(false);
            foreach (var questionItem in GetLocalizationDataList())
            {
                _questionItems.Add(questionItem);
            }
        
            Shuffle(_questionItems);
            CreateNewUI();
            _hideBlack.SetActive(false);
            ShowFullAds();
            return;
        }

        if (!SceneManager.GetActiveScene().name.Equals("AnimeSceneClicker"))
        {
            SceneManager.LoadScene("AnimeSceneClicker");
            return;
        }

        if (YandexGame.savesData.IsTestCompleted & !YandexGame.savesData.IsDateStarted)
        {
            //_startGamePG.SetActive(false);
            if(_endGamePG != null)
                _endGamePG.SetActive(true);
            if(_showWinner != null)
                _showWinner.SetWinner(YandexGame.savesData.GirlNumber);
            _hideBlack.SetActive(false);
            ShowFullAds();
            return;
        }
        
        if (YandexGame.savesData.IsTestCompleted & YandexGame.savesData.IsDateStarted)
        {
            //_startGamePG.SetActive(false);
            //_endGamePG.SetActive(true);
            if(_startDateButton != null)
                _startDateButton.SetActive(false);
            _showWinner.SetWinner(YandexGame.savesData.GirlNumber);
            _clickerObject.SetActive(true);
            _showWinner.ToDatePart();
            _showWinner.UndressGirlIfSave(YandexGame.savesData.GirlNumber);
            _clickerScore.ClicksCount = YandexGame.savesData.Score;
            _clickerScore.ClickMultiplayer = YandexGame.savesData.ScoreMultiplayer;
            _hideBlack.SetActive(false);
            ShowFullAds();
            return;
        }
        
        
        
        
    }

    private void ShowFullAds()
    {
        try
        {
            YandexGame.FullscreenShow();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private List<QuestionItem> GetLocalizationDataList()
    {
        Debug.Log((YandexGame.EnvironmentData.language.ToString()));
        //YandexGame.EnvironmentData.language = "tr";
        //YandexGame.EnvironmentData.language = "en";
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
        if(_isGameOver)
            return;
        
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
        if(_isGameOver)
            return;
        
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
        _isGameOver = true;
        _firstButton.interactable = false;
        _secoundButton.interactable = false;
        Debug.Log("GameCompleted" + GetIdWinGirl());
        YandexGame.savesData.GirlNumber = GetIdWinGirl();
        YandexGame.savesData.IsTestCompleted = true;
        YandexGame.SaveProgress();
        SceneManager.LoadScene("AnimeSceneClicker");
        
        
        // _startGamePG.SetActive(false);
        // _endGamePG.SetActive(true);
        // _showWinner.SetWinner();
    }

    private int GetIdWinGirl()
    {
        int maxSCore = _questionsList.Persons.Max(i => i.Score);
        return _questionsList.Persons.FindIndex(i => i.Score == maxSCore);
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

    public static void SendMetric(string name)
    {
        //YandexMetrica.Send(name);
    }
}