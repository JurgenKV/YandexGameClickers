using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ShowWinner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsList;
    [SerializeField] private List<Image> dressImages;
    
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
    }

    private void UndressButton()
    {
        try
        {
            // вкл рекламу и вырубить звук , затем включить (если был включен)
            dressImages[_indexOfGirl].enabled = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}