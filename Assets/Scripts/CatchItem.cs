using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum TypeItem
{
    Money,
    Health,
    Damage
}

public class CatchItem : MonoBehaviour
{
    public TypeItem TypeItem;
    
    [SerializeField]private Image _image;
    [SerializeField]private Sprite _heart;
    [SerializeField]private Sprite _damage;
    [SerializeField]private List<Sprite> _money;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private CatchController _catchController;
    private void Start()
    {
        _catchController = FindObjectOfType<CatchController>();
        GetComponent<Canvas>().worldCamera = Camera.main;
        if (TypeItem == TypeItem.Money)
        {
            _image.sprite = _money[Random.Range(0, _money.Count)];
        }
        
        if (TypeItem == TypeItem.Damage)
        {
            _image.sprite = _damage;
        }
        
        if (TypeItem == TypeItem.Health)
        {
            _image.sprite = _heart;
        }

        _rigidbody2D.gravityScale += (float)(_catchController.repeatCount / 100);
        
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        if(_catchController.IsGameOver)
            Destroy(gameObject);
    }
}