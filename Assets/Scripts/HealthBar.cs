using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    //[SerializeField] private GameObject _heartPrefab;
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
             hearthsAnimator.SetBool(IsFill, true);
        }

        _maxHealth = _hearthsAnimators.Count;
        _tempHealth = _hearthsAnimators.Count;
    }
    public void Damage()
    {
        _hearthsAnimators[_tempHealth - 1].SetBool(IsFill, false);
        _tempHealth--;
    }

    public void Regenerate()
    {
        if(_tempHealth.Equals(_maxHealth))
            return;
        _tempHealth++;
        _hearthsAnimators[_tempHealth - 1].SetBool(IsFill, true);
    }

    public bool IsMaxHealth()
    {
        return _tempHealth == _maxHealth;
    }
}