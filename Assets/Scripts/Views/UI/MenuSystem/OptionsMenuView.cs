using LabyrinthEscape.GameSettingsControls;
using UnityEngine;
using UnityEngine.UI;

namespace LabyrinthEscape.MenuSystem
{
    public class OptionsMenuView : MenuView
    {
        [SerializeField] private Text _isMusicEnabledText;

        [SerializeField] private Text _isSoundEnabledText;

        private bool _isMusicEnabled;

        private bool IsMusicEnabled
        {
            get { return _isMusicEnabled; }
            set
            {
                _isMusicEnabled = value;
                _isMusicEnabledText.text = _isMusicEnabled ? "On" : "Off";
            }
        }

        private bool _isSoundEnabled;

        private bool IsSoundEnabled
        {
            get { return _isSoundEnabled; }
            set
            {
                _isSoundEnabled = value;
                _isSoundEnabledText.text = _isSoundEnabled ? "On" : "Off";
            }
        }

        public override void Show(MenuView previousMenu = null)
        {
            base.Show(previousMenu);

            IsMusicEnabled = GameSettings.Instance.IsMusicEnable;
            IsSoundEnabled = GameSettings.Instance.IsSoundEnable;
        }

        public void OnOptionsSaveButtonClicked()
        {
            GameSettings.Instance.IsMusicEnable = IsMusicEnabled;
            GameSettings.Instance.IsSoundEnable = IsSoundEnabled;
            GameSettings.Instance.Save();
            OnBackButtonClicked();
        }

        public void ToggleMusic()
        {
            IsMusicEnabled = !IsMusicEnabled;
        }

        public void ToggleSound()
        {
            IsSoundEnabled = !IsSoundEnabled;
        }
    }
}