using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UpgradeRoom : MonoBehaviour
{
    [SerializeField] private ClickerScore _clickerScore;
    [SerializeField] private Button _button;
    [SerializeField] private int _cost = 250;
    [SerializeField] private int max = 7;
    [SerializeField] private List<GameObject> _images = new List<GameObject>();

    [SerializeField] private Button buttonObj;
    
    //[SerializeField] private SpriteType _spriteType;

    //private int _currentIndex = 0;
    private int _roomLevel = -1;
    private void Start()
    {
        LoadImage(YandexGame.savesData.roomLevel);
    }

    public void OnUpgradeRoom()
    {
        if (_clickerScore.ClicksCount < _cost)
            return;
        _clickerScore.ClicksCount -= _cost;
        _roomLevel++;
        LoadImage(_roomLevel);
        YandexGame.savesData.roomLevel = _roomLevel;
        YandexGame.SaveProgress();
    }

    public void LoadImage(int num)
    {
        _roomLevel = num;
        CheckMax();
        if (_roomLevel == -1)
        {
            return;
        }
        else
        {
            for (int i = 0; i <= _roomLevel; i++)
            {
                _images[i].SetActive(true);
            }
        }
        
    }

    public void ADSClick()
    {
        YGRewardedVideoManager.OpenRewardAd(4);
    }

    public void EndRewardADSUpgradeRoomClick()
    {
        _roomLevel++;
        LoadImage(_roomLevel);
        YandexGame.savesData.roomLevel = _roomLevel;
        YandexGame.SaveProgress();
        
        StartCoroutine(TimerBgCoroutine());
    }

    IEnumerator TimerBgCoroutine()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(30);
        _button.interactable = true;
    }

    private void CheckMax()
    {
        if (_roomLevel >= 7)
        {
            _roomLevel = 7;
            StopAllCoroutines();
            buttonObj.interactable = false;
            _button.interactable = false;
        }
    }
}
