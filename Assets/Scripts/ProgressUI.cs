using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyUI;
    
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
        _moneyUI.text = _levelUI.text = YandexGame.savesData.MoneyAmount.ToString() + "$";
    }

    private void RefreshLevelUI()
    {
        _levelUI.text = YandexGame.savesData.Level.ToString();
    }
    
    private void RefreshScorebarUI()
    {
        //_levelUI.text = YandexGame.savesData.Level.ToString();
        _slider.value = (int) (YandexGame.savesData.Experience / YandexGame.savesData.ExperienceToNextLevel * 100) + 10;
        _sliderTextUI.text = YandexGame.savesData.Experience.ToString() + "/" +
                             YandexGame.savesData.ExperienceToNextLevel.ToString();
    }
    
    

}