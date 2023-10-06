using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyUI;
    //[SerializeField] private Slider _slider;
    //[SerializeField] private TMP_Text _sliderTextUI;
    [SerializeField] private TMP_Text _score;

    public void RefreshAllUI()
    {
        RefreshMoneyUI();
        RefreshLevelUI();
       // RefreshScorebarUI();
    }
    
    private void Update()
    {
        RefreshMoneyUI();
    }

    public void RefreshMoneyUI()
    {
       // _moneyUI.text = _clickerScore.ClicksCount.ToString() + "$";
    }

    private void RefreshLevelUI()
    {
       // _score.text = _clickerScore.level.ToString();
    }

    public static float GetPercent(long a, long b)
    {
        if (a == 0) 
            return 0;
        
        return ( ((float)a / (float)b) * (float)100);
    }

}