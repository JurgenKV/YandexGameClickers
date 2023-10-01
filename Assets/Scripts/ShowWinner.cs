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
    [SerializeField] private List<GameObject> _objectsToOff;
    [SerializeField] private List<GameObject> _clicerObjectsToOn;
    private int _indexOfGirl;
    
    public void SetWinner(int girlNum = -1)
    {
       // Debug.Log(YandexGame.savesData.GirlNumber);
        if (girlNum == -1)
        {
            try
            {
                int maxScore = QuestionsList.Persons.Max(i => i.Score);
                _indexOfGirl = QuestionsList.Persons.FindIndex(i => i.Score.Equals(maxScore));
                objectsList[_indexOfGirl].SetActive(true);
                GameObject tempObj = objectsList[_indexOfGirl];
                tempObj.SetActive(true);
                foreach (GameObject o in objectsList)
                {
                    if(!o.Equals(tempObj))
                        Destroy(o);
                }
            }
            catch (Exception e)
            {
                Debug.Log("SetWinner error");
                Console.WriteLine(e);
                foreach (GameObject o in objectsList)
                {
                    o.SetActive(false);
                }

                _indexOfGirl = 0;
                objectsList[_indexOfGirl].SetActive(true);
            }
        }
        else
        {
            // foreach (GameObject o in objectsList)
            // {
            //     o.SetActive(false);
            // }
            objectsList[girlNum].SetActive(true);
            _indexOfGirl = girlNum;
            GameObject tempObj = objectsList[_indexOfGirl];
            tempObj.SetActive(true);
            foreach (GameObject o in objectsList)
            {
                if(!o.Equals(tempObj))
                    Destroy(o);
            }
        }
        

        
        YandexGame.SwitchLanguage(YandexGame.EnvironmentData.language);
    }

    public void UndressButton()
    {
        Debug.Log("_indexOfGirl" + _indexOfGirl);
        try
        {
            YandexGame.RewVideoShow(0);
            dressImages[_indexOfGirl].enabled = false;
            ADSButton.SetActive(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            dressImages[_indexOfGirl].enabled = false;
            ADSButton.SetActive(false);
        }

       // YandexGame.savesData.IsGirlUndressed = true;
        YandexGame.SaveProgress();
    }

    public void UndressGirlIfSave(int girlNum = -1)
    {
        try
        {
            // if (YandexGame.savesData.IsGirlUndressed)
            // {
            //     dressImages[girlNum].enabled = false;
            //     ADSButton.SetActive(false);
            // }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        
    }

    public void ToDatePart()
    {
        // foreach (GameObject o in _objectsToOff)
        // {
        //     try
        //     {
        //         o.SetActive(true);
        //         Destroy(o);
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //     }
        // }

        for (int i = 0; i < _objectsToOff.Count; i++)
        {
            if (_objectsToOff[i] != null)
            {
                _objectsToOff[i].SetActive(true);
                Destroy(_objectsToOff[i]);
            }
        }

        foreach (GameObject o in _clicerObjectsToOn)
        {
            o.SetActive(true);
        }
        _objectsToOff.Clear();
       // YandexGame.savesData.IsDateStarted = true;
        YandexGame.SaveProgress();
    }
}