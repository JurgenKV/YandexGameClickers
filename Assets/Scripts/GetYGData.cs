
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using YG;

    public class GetYGData : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private Animator _animator;
        
        private void OnEnable() => YandexGame.GetDataEvent += GetData;
        
        private void OnDisable() => YandexGame.GetDataEvent -= GetData;
        
        private void Awake()
        {
            if (YandexGame.SDKEnabled == true)
            {
                GetData();
                Debug.Log("SDKEnabled == true");
            }
        }
        
        private void GetData()
        {
            Debug.Log(YandexGame.savesData.MoneyAmount + "Score");
            _animator.SetTrigger("AnimEnd");
            Invoke(nameof(LoadGameEvent), 1);
        }
        

        public void LoadGameEvent()
        {
            SceneManager.LoadScene(_sceneName);
        }

    }
