using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;


public class ShowWinner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsList;
    [SerializeField] private List<Image> dressImages;
    [SerializeField] private GameObject ADSButton;
    [SerializeField] private QuestionsList QuestionsList;
    private int _indexOfGirl;
    
    public void SetWinner()
    {
        try
        {
            int maxScore = QuestionsList.Persons.Max(i => i.Score);
            int _indexOfGirl = QuestionsList.Persons.FindIndex(i => i.Score.Equals(maxScore));
            foreach (GameObject o in objectsList)
            {
                o.SetActive(false);
            }
            objectsList[_indexOfGirl].SetActive(true);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            foreach (GameObject o in objectsList)
            {
                o.SetActive(false);
            }

            _indexOfGirl = 0;
            objectsList[_indexOfGirl].SetActive(true);
        }

        
        YandexGame.SwitchLanguage(YandexGame.EnvironmentData.language);
    }

    public void UndressButton()
    {
        try
        {
            YandexGame.RewVideoShow(0);
            dressImages[_indexOfGirl].enabled = false;
            ADSButton.SetActive(false);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}