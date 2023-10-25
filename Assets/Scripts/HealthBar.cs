using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    //[SerializeField] private GameObject _heartPrefab;
    [SerializeField] private GameController _gameController;
    [SerializeField] private int _tempHealth;
    
    private int _maxHealth;
    public int TempHealth => _tempHealth;
    public int MaxHealth => _maxHealth;

    public List<Animator> _hearthsAnimators;
    
    private static readonly int IsFill = Animator.StringToHash("IsFill");
    
    public void Start()
    {
        foreach (Animator hearthsAnimator in _hearthsAnimators)
        {
            hearthsAnimator.SetBool(IsFill, false);
        }
        
        if (_gameController.HpBuster)
        {
            foreach (Animator hearthsAnimator in _hearthsAnimators)
            {
                hearthsAnimator.SetBool(IsFill, true);
            }
            _maxHealth = _hearthsAnimators.Count - 3;
            _tempHealth = _hearthsAnimators.Count;
        }
        else
        {
            for (int i = 0; i < _hearthsAnimators.Count - 3; i++)
            {
                _hearthsAnimators[i].SetBool(IsFill, true);
            }

            _maxHealth = _hearthsAnimators.Count - 3;
            _tempHealth = _hearthsAnimators.Count - 3;
        }
    }
    public void Damage()
    {
        _hearthsAnimators[_tempHealth - 1].SetBool(IsFill, false);
        _tempHealth--;
    }

    public void Regenerate()
    {
        if(_tempHealth >= _maxHealth)
            return;
        _tempHealth++;
        _hearthsAnimators[_tempHealth - 1].SetBool(IsFill, true);
    }

    public bool IsMaxHealth()
    {
        return _tempHealth >= _maxHealth;
    }
}