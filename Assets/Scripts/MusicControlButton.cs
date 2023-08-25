using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlButton : MonoBehaviour
{
    [SerializeField] private AudioSource _backMusic;
    [SerializeField] Toggle _toggle;
    [SerializeField] private Image _image;

    [SerializeField] private Sprite _onImage;
    [SerializeField] private Sprite _offImage;

    private float _startVolume = 0;
    
    private bool _isFadeInActive = false;
    private bool _isFadeOutActive = false;
    void Start()
    {
        _startVolume = _backMusic.volume;
        _toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_toggle);
        });
    }
    
    void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            if(_isFadeOutActive || _isFadeInActive)
            {
                change.isOn = false;
                return;
            }
            
            StartCoroutine(MusicFadeIn(_backMusic));
        }
        else
        {
            if(_isFadeOutActive || _isFadeInActive)
            {
                change.isOn = true;
                return;
            }
            
            StartCoroutine(MusicFadeOut(_backMusic));
        }
    }

    private IEnumerator MusicFadeIn(AudioSource audioSource)
    {
        _isFadeInActive = true;
        _image.sprite = _onImage;
        while (audioSource.volume < _startVolume)
        {
            audioSource.volume += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _isFadeInActive = false;
    }

    private IEnumerator MusicFadeOut(AudioSource audioSource)
    {
        _isFadeOutActive = true;
        _image.sprite = _offImage;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _isFadeOutActive = false;
    }
}