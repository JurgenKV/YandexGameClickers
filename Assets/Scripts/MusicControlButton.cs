using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class MusicControlButton : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource _backMusic;
    [SerializeField] Toggle _toggle = null;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _onImage;
    [SerializeField] private Sprite _offImage;
    
    [Header("Sound")]
    [SerializeField] Toggle _toggle2 = null;
    [SerializeField] private Image _image2;
    [SerializeField] private Sprite _onImage2;
    [SerializeField] private Sprite _offImage2;
    private float _startVolume = 0;
    
    void Start()
    {
        _startVolume = _backMusic.volume;
        
        MusicChange(YandexGame.savesData.IsMusicEnabled);
        SoundChange(YandexGame.savesData.IsSoundEnabled);
    }

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
    
    public void SoundChange(bool change = true)
    {
        try
        {
            if (change)
            {
                if(_toggle2 != null)
                    _toggle2.isOn = true;
                Debug.Log("On Sound");
                if(_image2 != null)
                    _image2.sprite = _onImage2;
            }
            else
            {
                if(_toggle2 != null)
                    _toggle2.isOn = false;
                
                if(_image2 != null)
                    _image2.sprite = _offImage2;
                Debug.Log("Off Sound");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        YandexGame.savesData.IsSoundEnabled = change;
        YandexGame.SaveProgress();
    }
}