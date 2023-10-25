
    using System;
    using UnityEngine;
    using YG;

    public class YGRewardedVideoManager : MonoBehaviour
    {
       // [SerializeField] private ClickerScore _clickerScore;
       [SerializeField] private ShopManager _shopManager = null;
       [SerializeField] private GameController _gameController = null;
       [SerializeField] private BusterMenu _busterMenu;
        private static int _rewardIndex;

        private static int indexOfSoldItem = -1;
        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;
            //YandexGame.ErrorVideoEvent += RewardedError;
        }

        // Отписываемся от события открытия рекламы в OnDisable
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
            //YandexGame.ErrorVideoEvent -= RewardedError;
        }

        // Подписанный метод получения награды
        private void Rewarded(int id)
        {
            switch (id)
            {
                case 1:
                    if(_shopManager == null)
                        return;
                    
                    _shopManager.RewardEndSoldItem(indexOfSoldItem);
                    indexOfSoldItem = -1;
                   
                    Debug.Log("EndReward");
                   
                    break;
                case 2:
                    _gameController.EndRewardSlowTime();
                    Debug.Log("EndReward");
                    break;
                case 3:
                   _gameController.EndRewardHealth();
                    Debug.Log("EndReward");
                    break;
                case 4:
                    _busterMenu.ADSButtonBuyHealthEND();
                    Debug.Log("EndReward");
                    break;
                case 5:
                    _busterMenu.ADSButtonBuyMoneyEND();
                    Debug.Log("EndReward");
                    break;
                case 6:
                    _busterMenu.ADSButtonBuyScoreEND();
                    Debug.Log("EndReward");
                    break;
            }
        }
        
        private void RewardedError()
        {
            switch (_rewardIndex)
            {
                case 1:
                    Debug.Log("EndReward RewardedError");
                    break;
                case 2:
                    _gameController.EndRewardSlowTime();
                    Debug.Log("EndReward");
                    break;
                case 3:
                    _gameController.EndRewardHealth();
                    Debug.Log("EndReward");
                    break;
            }
        }

        // Метод для вызова видео рекламы
        public static void OpenRewardAd(int id)
        {
            try
            {
                YandexGame.RewVideoShow(id);
                _rewardIndex = id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void OpenRewardAdWithSoldItem(int id, int soldItemIndex)
        {
            indexOfSoldItem = soldItemIndex;
            try
            {
                YandexGame.RewVideoShow(id);
                _rewardIndex = id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static void ShowFullAds()
        {
            try
            {
                YandexGame.FullscreenShow();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
