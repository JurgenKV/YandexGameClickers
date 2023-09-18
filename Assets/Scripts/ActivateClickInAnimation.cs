using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;
using Random = UnityEngine.Random;
using TouchPhase = UnityEngine.TouchPhase;


public class ActivateClickInAnimation : MonoBehaviour
{
    [SerializeField] private ClickerScore _clickerScore;
    private Animator _animator;
    private Vector3 _currentPosition;

    [SerializeField] private List<AudioSource> _audioSources;
    private static readonly int Scale = Animator.StringToHash("Scale");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ClickEvent()
    {
        if (_clickerScore == null)
            return;

        if (gameObject.activeSelf)
            _clickerScore.Click();
    }

    public void PlayAnim()
    {
        if (_animator == null)
            return;

        if (_clickerScore == null)
            return;

        if (!_clickerScore.gameObject.activeSelf)
            return;
        Instantiate(_clickerScore.ParticleSystems[Random.Range(0, _clickerScore.ParticleSystems.Count)],
            _currentPosition, Quaternion.identity);
        //PlayEggSound();
        //tempObj.AddComponent<ParticleTimer>();
        _animator.SetTrigger(Scale);
    }

    public void PlayEggSoundEvent()
    {
        if (YandexGame.savesData.IsSoundEnabled & !YandexGame.savesData.IsAnimal)
        {
            if (_audioSources.TrueForAll(i => !i.isPlaying))
            {
                _audioSources[Random.Range(0,_audioSources.Count)].Play();
            }
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;

        // if (e.type == EventType.MouseDown && e.button == 0) 
        // {
        //     Vector2 clickPosition = e.mousePosition;
        //     _currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickPosition.x, clickPosition.y, 1));
        //
        //     Debug.Log(_currentPosition);
        // }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 inputPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2) Input.mousePosition;
            _currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 1));
        }
    }
}