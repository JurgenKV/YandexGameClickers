
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using YG;

    public class GetYGData : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        private void OnEnable() => YandexGame.GetDataEvent += GetData;

        private void GetData()
        {
            SceneManager.LoadScene(_sceneName);
        }
        
    }
