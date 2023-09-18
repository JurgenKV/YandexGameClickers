
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using YG;

    public class GetYGData : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        private void OnEnable() => YandexGame.GetDataEvent += GetData;

        private void GetData()
        {
            Debug.Log(YandexGame.savesData.MoneyScore + "Score");
            SceneManager.LoadScene(_sceneName);
        }
        
    }
