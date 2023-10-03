// using System;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using YG;
//
//
// public class CollectionManager : MonoBehaviour
// {
//     [SerializeField] private List<CollectionItem> _collectionItems;
//     
//     [SerializeField] private Image _image;
//     [SerializeField] private Material _greyMaterial;
//     [SerializeField] private TMP_Text _textName;
//     
//     [SerializeField] private TMP_Text _textNumber;
//     [SerializeField] private TMP_Text _textCopyAmount;
//     [SerializeField] private int _currentIndex = 0;
//     private void Start()
//     {
//         UpdateUI(_currentIndex);
//     }
//
//     private void UpdateUI(int num)
//     {
//         _currentIndex = num;
//         
//         _image.sprite = _collectionItems[num].Sprite;
//         _textCopyAmount.text = GetCopyAmountText(num);
//         if (YandexGame.savesData.PusheenNums.Contains(num))
//         {
//             _image.material = null;
//         }
//         else
//             _image.material = _greyMaterial;
//         
//         if (YandexGame.savesData.language.Equals("ru"))
//             _textName.text = _collectionItems[num].NameRu;
//         
//         if (YandexGame.savesData.language.Equals("en"))
//             _textName.text = _collectionItems[num].NameEn;
//
//         _textNumber.text = (num + 1) + "/" + _collectionItems.Count;
//     }
//
//     private string GetCopyAmountText(int num)
//     {
//         if (YandexGame.savesData.PusheenNums.Contains(num))
//         {
//             int count = YandexGame.savesData.PusheenNums.FindAll(i => i == num).Count;
//             
//             if(count > 1)
//                 return "X" + YandexGame.savesData.PusheenNums.FindAll(i => i == num).Count.ToString();
//         }
//
//         return "";
//     }
//
//     public void NextItem()
//     {
//         int num = _currentIndex + 1;
//         
//         if (num > _collectionItems.Count - 1)
//             UpdateUI(0);
//         else
//             UpdateUI(num);
//     }
//
//     public void PrevItem()
//     {
//         int num = _currentIndex - 1;
//
//         if (num < 0)
//             UpdateUI(_collectionItems.Count - 1);
//         else
//             UpdateUI(num);
//     }
// }
//
// [Serializable]
// public class CollectionItem
// {
//     public Sprite Sprite;
//     public string NameRu;
//     public string NameEn;
//     //public bool IsLock = false;
//
// }