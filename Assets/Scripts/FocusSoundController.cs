using System;
using UnityEngine;
using YG;


public class FocusSoundController : MonoBehaviour
{
    private float _volume;
    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
    }

    private void Awake()
    {
        _volume = AudioListener.volume;
        CheckADS();
    }

    private void Update()
    {
        CheckADS();
    }

    private void CheckADS()
    {
        if (YandexGame.nowFullAd || YandexGame.nowVideoAd)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = _volume;
        }
    }
}