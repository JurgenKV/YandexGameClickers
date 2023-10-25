using System;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class CatController : MonoBehaviour
{
    [SerializeField] private CatchController catchController;
    [SerializeField] private List<AudioSource> _audioSources;

    private void OnTriggerEnter2D(Collider2D col)
    {
        CatchItem catchItem = col.GetComponent<CatchItem>();

        if (catchItem == null)
            return;
        
        if (catchItem.TypeItem == TypeItem.Money)
        {
            catchController.ClicksCount += catchController.ClickMultiplayer * 2;
        }
        
        if (catchItem.TypeItem == TypeItem.Damage)
        {
            catchController.HealthBar.Damage();
        }
        
        if (catchItem.TypeItem == TypeItem.Health)
        {
            catchController.HealthBar.Regenerate();
        }
        
        Instantiate(catchController.ParticleSystems[Random.Range(0, catchController.ParticleSystems.Count)],
            catchItem.transform.position, Quaternion.identity);
        Destroy(col.gameObject);
        PlaySound();
    }
    
    private void PlaySound()
    {
        if (YandexGame.savesData.IsSoundEnabled)
        {
            if (_audioSources.TrueForAll(i => !i.isPlaying))
            {
                _audioSources[Random.Range(0,_audioSources.Count)].Play();
            }
        }
    }
}