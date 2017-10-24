using UnityEngine;

namespace LabyrinthEscape.GameSettingsControls
{
    public class GameSettings
    {
        #region Singleton

        private GameSettings()
        {
        }

        private static GameSettings _instance;

        public static GameSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSettings();
                    _instance.Load();
                }

                return _instance;
            }
        }

        #endregion

        public bool IsMusicEnable;

        public bool IsSoundEnable;

        public void Load()
        {
            // если в плеерпрефсах нет такого значения, значит настройки не были ни разу созданы, и по умолчанию должно
            // считаться что звук и музыка включены. Поэтому defaultvalue указан как 1
            IsMusicEnable = PlayerPrefs.GetInt("Settings_IsMusicEnable", 1) == 1;
            IsSoundEnable = PlayerPrefs.GetInt("Settings_IsSoundEnable", 1) == 1;
        }

        public void Save()
        {
            PlayerPrefs.SetInt("Settings_IsMusicEnable", IsMusicEnable ? 1 : 0);
            PlayerPrefs.SetInt("Settings_IsSoundEnable", IsSoundEnable ? 1 : 0);
        }
    }
}