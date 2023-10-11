using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ItemsSO _itemsTemplate;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private TMP_Text _pageText;
    [SerializeField] private GameObject _nextPageButton;
    [SerializeField] private GameObject _prevPageButton;

    private List<ItemsSO.Item> _items = new List<ItemsSO.Item>();

    [SerializeField] private List<GameObject> gridObjects = new List<GameObject>();

    //[SerializeField] private int pageNumber = 0;
    [SerializeField] private int minPg = 0;
    [SerializeField] private int maxPg = 6;
    [SerializeField] private int _maxUIPage = 4;
    
    private void Start()
    {
        _items.AddRange(_itemsTemplate.Items);
        CheckSoldItemsYG();
        CheckActiveItemsYG();
        //FindObjectOfType<ProgressUI>().Money = 10000;
        _prevPageButton.SetActive(false);
        CreateGrid(0, 6);
    }

    public void NextPage()
    {
        DestroyGridObjects();
        CreateGrid(minPg += 6, maxPg += 6);
        
        if ((maxPg / 6) == _maxUIPage)
        {
            _nextPageButton.SetActive(false);
        }
        
        _prevPageButton.SetActive(true);
        _pageText.text = (maxPg / 6).ToString() + "/" + _maxUIPage.ToString();
    }

    public void PrevPage()
    {
        
        DestroyGridObjects();
        CreateGrid(minPg -= 6, maxPg -= 6);
        
        if ((maxPg / 6) == 1)
        {
            _prevPageButton.SetActive(false);
        }
        
        _pageText.text = (maxPg / 6).ToString() + "/" + _maxUIPage.ToString();
        _nextPageButton.SetActive(true);
    }

    private void CheckSoldItemsYG()
    {
        foreach (int soldItem in YandexGame.savesData.SoldItems)
        {
            _items.Find(i => i.Index == soldItem).IsSold = true;
        }
    }

    private void CheckActiveItemsYG()
    {
        foreach (int activeItem in YandexGame.savesData.ActiveItems)
        {
            _items.Find(i => i.Index == activeItem).IsActive = true;
        }
    }

    private void CreateGrid(int minInclude, int maxExclude)
    {
        for (int i = minInclude; i < maxExclude; i++)
        {
            if (_items.Count == i)
                return;

            GameObject newGridItem = Instantiate(itemPrefab, new Vector3(0, 0, 1), quaternion.identity, transform);
            newGridItem.GetComponent<ShopItem>().GenerateSlot(_items[i]);
            gridObjects.Add(newGridItem);
        }
    }

    public void RewardEndSoldItem(int indexArg)
    {
        foreach (GameObject gridObject in gridObjects)
        {
            if (gridObject.GetComponent<ShopItem>().ItemData.Index == indexArg)
                gridObject.GetComponent<ShopItem>().BuyByRewardEnd();
        }
    }

    public void SaveChanges(ItemsSO.Item changesItem)
    {
        int indexArg = _items.Find(i => i.Index == changesItem.Index).Index;

        int index = _items.IndexOf(changesItem);
        _items[index] = changesItem;

        if (!YandexGame.savesData.SoldItems.Contains(indexArg) && changesItem.IsSold)
            YandexGame.savesData.SoldItems.Add(indexArg);

        if (YandexGame.savesData.ActiveItems.Contains(indexArg))
        {
            if (!_items[index].IsActive)
                YandexGame.savesData.ActiveItems.Remove(indexArg);
        }
        else
        {
            if (_items[index].IsActive)
                YandexGame.savesData.ActiveItems.Add(indexArg);
        }

        YandexGame.SaveProgress();
    }

    private void DestroyGridObjects()
    {
        foreach (GameObject gridObject in gridObjects)
        {
            Destroy(gridObject);
        }
        gridObjects.Clear();
    }

    public bool IsHasOneActiveItem()
    {
        if (_items.FindAll(i=> i.IsActive).Count == 1)
            return true;

        return false;
    }
}