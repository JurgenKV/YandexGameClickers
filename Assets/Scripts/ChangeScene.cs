using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void ChangeSceneButton()
    {
        SceneManager.LoadScene(_sceneName);
    }
}