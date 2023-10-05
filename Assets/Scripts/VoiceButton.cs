// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.UI;
// using Random = UnityEngine.Random;
//
// public class VoiceButton : MonoBehaviour
// {
//     [SerializeField] private int _cost;
//     
//     [SerializeField] private List<AudioSource> _sources = new List<AudioSource>();
//
//     [SerializeField] private List<AudioSource> _sources2 = new List<AudioSource>();
//     [SerializeField] private GameObject parenSource2 = null;
//
//     private bool isMusicOver = true;
//     private void Start()
//     {
//         _sources = gameObject.GetComponents<AudioSource>().ToList();
//         if(parenSource2 != null)
//             _sources2 = parenSource2.GetComponents<AudioSource>().ToList();
//     }
//
//     public void OnVoice()
//     {
//         if (_clickerScore.ClicksCount < _cost)
//             return;
//
//         if (_sources.All(i => !i.isPlaying))
//         {
//             _clickerScore.ClicksCount -= _cost;
//             _sources[Random.Range(0, _sources.Count)].Play();
//         }
//     }
//     
//     public void OnSlap()
//     {
//         if (_clickerScore.ClicksCount < _cost)
//             return;
//
//         if (_sources.All(i => !i.isPlaying) & _sources2.All(i => !i.isPlaying) & isMusicOver)
//         {
//             isMusicOver = false;
//             _clickerScore.ClicksCount -= _cost;
//             _sources[Random.Range(0, _sources.Count)].Play();
//             Invoke(nameof(SlapContinue), 1f);
//         }
//         
//         
//     }
//
//     private void SlapContinue()
//     {
//         _sources2[Random.Range(0, _sources2.Count)].Play();
//         isMusicOver = true;
//     }
// }