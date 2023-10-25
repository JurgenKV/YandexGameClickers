﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSO", menuName = "Game/ItemsSO")]

[Serializable]
public class ItemsSO : ScriptableObject
{
    [SerializeField] public List<Item> Items;
    
    [Serializable]
    public class Item
    {
        public int Index;
        public Sprite Sprite;
        public int Cost;
        public bool IsRewardCost = false;
        public bool IsActive = false;
        public bool IsSold = false;
    }
}
