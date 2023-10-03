
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        // public int money = 1;                       // Можно задать полям значения по умолчанию
        // public string newPlayerName = "Hello!";
        // public bool[] openLevels = new bool[3];
       
        public long MoneyAmount = 0;
        public int ClickMultiplayer = 1;
        
        public int Level = 1;
        public long Experience = 0;
        public long ExperienceToNextLevel = 0;
        
        //public int ObjectImageNum = 0;
        public int roomLevel = -1;
        public int catType = 0;
        // public List<int> PusheenNums = new List<int>();
        public bool IsMusicEnabled = true;
        public bool IsSoundEnabled = true;

        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            //openLevels[1] = true;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }

        public static void ResetPlayerData()
        {
            YandexGame.savesData.MoneyAmount = 0;
            YandexGame.savesData.ClickMultiplayer = 1;
            YandexGame.savesData.Experience = 0;
            YandexGame.savesData.ExperienceToNextLevel = 1000;
            YandexGame.savesData.Level = 1;
            YandexGame.savesData.roomLevel = -1;
            YandexGame.savesData.catType = 0;
            GameController.SetLeaderboard(0);
            YandexGame.SaveProgress();
            Debug.Log("ResetPlayerData");
        }
    }
}
