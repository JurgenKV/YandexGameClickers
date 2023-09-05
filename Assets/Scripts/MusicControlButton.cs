using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlButton : MonoBehaviour
{
    [SerializeField] private AudioSource _backMusic;
    [SerializeField] Toggle _toggle;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _onImage;
    [SerializeField] private Sprite _offImage;
    
    [SerializeField] Toggle _toggle2;
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
        _toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_toggle);
        });
        
        _toggle2.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_toggle2);
        });
    }
    void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            _toggle2.isOn = _toggle.isOn = true;
            Debug.Log("On Music");
            MusicFadeIn(_backMusic);
        }
        else
        {
            _toggle2.isOn = _toggle.isOn = false;
            Debug.Log("Off Music");
            MusicFadeOut(_backMusic);
        }
    }

    private void MusicFadeIn(AudioSource audioSource)
    {
        _image.sprite = _onImage;
        _image2.sprite = _onImage2;
        audioSource.mute = false;
        _tempMusicSettings = true;
    }

    private void MusicFadeOut(AudioSource audioSource)
    {
        _image.sprite = _offImage;
        _image2.sprite = _offImage2;
        audioSource.mute = true;
        _tempMusicSettings = false;
    }
}