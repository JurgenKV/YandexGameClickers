using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "QuestionsList", menuName = "QuestionsList")]
public class QuestionsList : ScriptableObject
{
    [SerializeField] public List<Person> Persons;
    [SerializeField] public List<QuestionItem> QuestionItems;

    public void NewGame()
    {
        foreach (Person person in Persons)
        {
            person.Score = 0;
        }
    }
}

[Serializable]
public class Person
{
    public int Id;
    public string Type;
    public string Description;
    public int Score;
}

[Serializable]
public class QuestionItem
{
    public int Id;
    public string QuestionText;
    
    public string VariantFirst;
    public int PersonIdFirst;
    
    public string VariantSecond;
    public int PersonIdSecond;
}

