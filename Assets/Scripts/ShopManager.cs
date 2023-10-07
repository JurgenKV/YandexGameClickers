using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ItemsSO _itemsTemplate;
    [SerializeField] private GameObject itemPrefab;
    
    private List<ItemsSO.Item> _items = new List<ItemsSO.Item>();

    [SerializeField] private List<GameObject> gridObjects = new List<GameObject>();
    private void Start()
    {
        _items.AddRange(_itemsTemplate.Items);

        foreach (ItemsSO.Item item in YandexGame.savesData.Items)
        {
            if (_items.Contains(item))
            { 
                int index = _items.FindIndex(i => i.Index == item.Index);
                _items[index] = item;
            }
        }

        CreateGrid(0,6);
    }

    private void CreateGrid(int minInclude, int maxExclude)
    {
        for (int i = minInclude; i < maxExclude; i++)
        {
            GameObject newGridItem = Instantiate(itemPrefab, new Vector3(0,0,1), quaternion.identity, transform);
            newGridItem.GetComponent<ShopItem>().GenerateSlot(_items[i]);
            gridObjects.Add(newGridItem);
        }
    }

    public void SaveChanges(ItemsSO.Item changesItem)
    {
        int index = _items.IndexOf(changesItem);
        _items[index] = changesItem;

        if (YandexGame.savesData.Items.Contains(_items[index]))
        {
            int YGindex = YandexGame.savesData.Items.IndexOf(_items[index]);
            YandexGame.savesData.Items[YGindex] = changesItem;
        }
        else
        {
            YandexGame.savesData.Items.Add(_items[index]);
        }
        YandexGame.SaveProgress();
        
    }
}