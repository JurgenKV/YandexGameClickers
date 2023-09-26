
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using YG;

    public class GetYGData : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        private void OnEnable() => YandexGame.GetDataEvent += GetData;

        private void GetData()
        {
            Debug.Log(YandexGame.savesData.MoneyAmount + "Score");
            SceneManager.LoadScene(_sceneName);
        }
        
    }
