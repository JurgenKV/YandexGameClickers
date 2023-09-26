using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyUI;
    [SerializeField] private ClickerScore _clickerScore;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _sliderTextUI;
    [SerializeField] private TMP_Text _levelUI;

    public void RefreshAllUI()
    {
        RefreshMoneyUI();
        RefreshLevelUI();
        RefreshScorebarUI();
    }

    public void RefreshMoneyUI()
    {
        _moneyUI.text = _clickerScore.ClicksCount.ToString() + "$";
    }

    private void RefreshLevelUI()
    {
        _levelUI.text = _clickerScore.level.ToString();
    }
    
    private void RefreshScorebarUI()
    {
        //_levelUI.text = YandexGame.savesData.Level.ToString();
        _slider.value = (int) (_clickerScore.experience / _clickerScore.experienceToNextLevel * 100) + 10;
        _sliderTextUI.text = _clickerScore.experience.ToString() + "/" +
                             _clickerScore.experienceToNextLevel.ToString();
    }
    
    

}