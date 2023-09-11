using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MusicControlButton : MonoBehaviour
{
    [SerializeField] private AudioSource _backMusic;
    [SerializeField] Toggle _toggle = null;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _onImage;
    [SerializeField] private Sprite _offImage;

    private float _startVolume = 0;
    
    void Start()
    {
        _startVolume = _backMusic.volume;
        // _toggle.isOn = YandexGame.savesData.IsMusicEnabled;
        //
        // if (_toggle != null)
        // {
        //     _toggle.onValueChanged.AddListener(delegate {
        //         ToggleValueChanged(_toggle);
        //     });
        // }
        
        //ToggleValueChanged(_toggle);
        MusicChange(YandexGame.savesData.IsMusicEnabled);
    }
    // void ToggleValueChanged(Toggle change)
    // {
    //     if(change == null)
    //         return;
    //     
    //     try
    //     {
    //         if (change.isOn)
    //         {
    //             if(_toggle != null)
    //                 _toggle.isOn = true;
    //             
    //             Debug.Log("On Music");
    //             MusicFadeIn(_backMusic);
    //         }
    //         else
    //         {
    //             if(_toggle != null)
    //                 _toggle.isOn = false;
    //         
    //             Debug.Log("Off Music");
    //             MusicFadeOut(_backMusic);
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //     }
    //
    //     YandexGame.savesData.IsMusicEnabled = change;
    //     YandexGame.SaveProgress();
    // }

    private void MusicFadeIn(AudioSource audioSource)
    {
        if(_image != null)
            _image.sprite = _onImage;

        audioSource.mute = false;
    }

    private void MusicFadeOut(AudioSource audioSource)
    {
        if(_image != null)
            _image.sprite = _offImage;

        audioSource.mute = true;
    }

    public void MusicChange(bool change = true)
    {

        try
        {
            if (change)
            {
                if(_toggle != null)
                    _toggle.isOn = true;
                
                Debug.Log("On Music");
                MusicFadeIn(_backMusic);
            }
            else
            {
                if(_toggle != null)
                    _toggle.isOn = false;
            
                Debug.Log("Off Music");
                MusicFadeOut(_backMusic);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        YandexGame.savesData.IsMusicEnabled = change;
        YandexGame.SaveProgress();
    }
}