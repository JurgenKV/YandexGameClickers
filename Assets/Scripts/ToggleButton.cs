using UnityEngine;


public class ToggleButton : MonoBehaviour
{
    //[SerializeField] private GameObject _gameObject;
    public void ChangeActivityOnClick(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}