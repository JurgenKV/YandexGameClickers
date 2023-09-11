using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlButton : MonoBehaviour
{
    [SerializeField] private AudioSource _backMusic;
    [SerializeField] Toggle _toggle = null;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _onImage;
    [SerializeField] private Sprite _offImage;
    
    [SerializeField] Toggle _toggle2 = null;
    [SerializeField] private Image _image2;
    [SerializeField] private Sprite _onImage2;
    [SerializeField] private Sprite _offImage2;

    private float _startVolume = 0;
    
    private bool _isFadeInActive = false;
    private bool _isFadeOutActive = false;
    private bool _tempMusicSettings = true;
    void Start()
    {
        _startVolume = _backMusic.volume;
        
        if (_toggle != null)
        {
            _toggle.onValueChanged.AddListener(delegate {
                ToggleValueChanged(_toggle);
            });
        }
        
        if (_toggle2 != null)
        {
            _toggle2.onValueChanged.AddListener(delegate {
                ToggleValueChanged(_toggle2);
            });
        }
            
    }
    void ToggleValueChanged(Toggle change)
    {
        if(change == null)
            return;
        
        try
        {
            if (change.isOn)
            {
                if(_toggle2 != null)
                    _toggle2.isOn = true;
            
                if(_toggle != null)
                    _toggle.isOn = true;
                Debug.Log("On Music");
                MusicFadeIn(_backMusic);
            }
            else
            {
                if(_toggle2 != null)
                    _toggle2.isOn = false;
            
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
        
    }

    private void MusicFadeIn(AudioSource audioSource)
    {
        if(_image != null)
            _image.sprite = _onImage;
        
        if(_image2 != null)
            _image2.sprite = _onImage2;
        
        audioSource.mute = false;
        _tempMusicSettings = true;
    }

    private void MusicFadeOut(AudioSource audioSource)
    {
        if(_image != null)
            _image.sprite = _offImage;
        
        if(_image2 != null)
            _image2.sprite = _offImage2;
        
        audioSource.mute = true;
        _tempMusicSettings = false;
    }
}