using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionsList _questionsList;
    [SerializeField] private GameObject prafabQuestionUI;
    [SerializeField] private Transform rootParent;

    [SerializeField] private QuestionUI _questionUis;
    [SerializeField] private QuestionsList _questionsListCopy;
    private void Start()
    {
        _questionsListCopy = new QuestionsList();
        _questionsListCopy.Persons.CopyTo(_questionsList.Persons.ToArray());
        _questionsListCopy.QuestionItems.CopyTo(_questionsList.QuestionItems.ToArray());
        Shuffle(_questionsListCopy.QuestionItems);
        CreateNewUI();
    }

    private void CreateNewUI()
    {
        _questionUis.CreateUI(_questionsListCopy.QuestionItems.First());

    }

    public void HandleButtonVariantFirst(QuestionItem questionItem)
    {
        _questionsListCopy.Persons.First(i => i.Id.Equals(questionItem.PersonIdFirst)).Score++;
        _questionsListCopy.QuestionItems.Remove(questionItem);
        CreateNewUI();
    }
    
    public void HandleButtonVariantSecond(QuestionItem questionItem)
    {
        _questionsListCopy.Persons.First(i => i.Id.Equals(questionItem.PersonIdFirst)).Score++;
        _questionsListCopy.QuestionItems.Remove(questionItem);
        CreateNewUI();
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

    private void OnDestroy()
    {
        Destroy(_questionsListCopy);
    }
}