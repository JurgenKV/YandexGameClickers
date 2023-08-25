using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionsList _questionsList;
    [SerializeField] private QuestionUI _questionUis;
    [SerializeField] public List<QuestionItem> _questionItems = new List<QuestionItem>();
    private void Start()
    {
        _questionItems.CopyTo(_questionsList.QuestionItems.ToArray());
        Shuffle(_questionItems);
        CreateNewUI();
    }

    private void CreateNewUI()
    {
        _questionUis.CreateUI(_questionItems.First());

    }

    public void HandleButtonVariantFirst(QuestionItem questionItem)
    {
        _questionsList.Persons.First(i => i.Id.Equals(questionItem.PersonIdFirst)).Score++;
        _questionItems.Remove(questionItem);
        CreateNewUI();
    }
    
    public void HandleButtonVariantSecond(QuestionItem questionItem)
    {
        _questionsList.Persons.First(i => i.Id.Equals(questionItem.PersonIdSecond)).Score++;
        _questionItems.Remove(questionItem);
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
}