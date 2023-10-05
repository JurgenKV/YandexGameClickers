
    using System;
    using UnityEngine;
    using YG;

    public class YGRewardedVideoManager : MonoBehaviour
    {
       // [SerializeField] private ClickerScore _clickerScore;
        //[SerializeField] private ChangeImage _changeImage;

        private static int _rewardIndex;
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
                   // _clickerScore.EndRewardStartTimerX2Coroutine();
                    Debug.Log("EndReward");
                    break;
                case 2:
                   // _clickerScore.EndRewardUpgradeClick();
                    Debug.Log("EndReward");
                    break;
                case 3:
                   // _changeImage.EndRewardADSBgClick();
                    Debug.Log("EndReward");
                    break;
            }
        }
        
        private void RewardedError()
        {
            switch (_rewardIndex)
            {
                case 1:
                   // _clickerScore.EndRewardStartTimerX2Coroutine();
                    Debug.Log("EndReward RewardedError");
                    break;
                case 2:
                  //  _clickerScore.EndRewardUpgradeClick();
                    Debug.Log("EndReward RewardedError");
                    break;
                case 3:
                   // _changeImage.EndRewardADSBgClick();
                    Debug.Log("EndReward RewardedError");
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
    }
