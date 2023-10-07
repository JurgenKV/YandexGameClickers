using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image Image;
    public Image ActivityImage;
    //public Button ButtonActive;
    public Button ButtonShop;
    public TMP_Text Price;
    public Image RewardImagePrice;
    
    public ItemsSO.Item ItemData;

    private ShopManager _shopManager;
    private void Start()
    {
        _shopManager = FindObjectOfType<ShopManager>();
        try
        {
            GetComponent<Canvas>().worldCamera = Camera.current;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        transform.position = new Vector3(0, 0, 1);
    }

    public void GenerateSlot(ItemsSO.Item itemData)
    {
        ItemData = itemData;
        Image.sprite = ItemData.Sprite;
        ActivityImage.sprite = ItemData.Sprite;
        if (!ItemData.IsSold)
        {
            ButtonShop.gameObject.SetActive(true);
            if (!ItemData.IsRewardCost)
            {
                Price.gameObject.SetActive(true);
                Price.text = ItemData.Cost.ToString() + "$";
                RewardImagePrice.gameObject.SetActive(false);
            }
            else
            {
                RewardImagePrice.gameObject.SetActive(true);
                Price.gameObject.SetActive(false);
            }

            ActivityImage.enabled = false;
        }
        else
        {
            ButtonShop.gameObject.SetActive(false);
            ActivityImage.enabled = ItemData.IsActive;
        }
    }

    public void BuyItem()
    {
        if(ItemData.IsSold)
            return;

        if (ItemData.IsRewardCost)
        {
            FindObjectOfType<YGRewardedVideoManager>().OpenRewardAdWithSoldItem(1,ItemData.Index );
        }
        else
        {
            ProgressUI progressUI = FindObjectOfType<ProgressUI>();
            if (progressUI.Money >= ItemData.Cost)
            {
                progressUI.Money -= ItemData.Cost;
                ButtonShop.gameObject.SetActive(false);
                ItemData.IsSold = true;
                ItemData.IsActive = true;
                ActivityImage.enabled = true;
                _shopManager.SaveChanges(ItemData);
            }
        }
    }

    public void BuyByRewardEnd()
    {
        ButtonShop.gameObject.SetActive(false);
        ItemData.IsSold = true;
        ItemData.IsActive = true;
        ActivityImage.enabled = true;
        _shopManager.SaveChanges(ItemData);
    }

    public void SelectToggleItem()
    {
        if(!ItemData.IsSold)
            return;

        if (!ItemData.IsActive)
        {
            ItemData.IsActive = true;
            ActivityImage.enabled = true;
        }
        else
        {
            ItemData.IsActive = false;
            ActivityImage.enabled = false;
        }
        
        _shopManager.SaveChanges(ItemData);
    }
}