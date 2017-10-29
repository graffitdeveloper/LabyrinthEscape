using System;
using Assets.Scripts.Views;
using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.LabyrinthGeneratorControls;
using UnityEngine;
using UnityEngine.UI;

namespace LabyrinthEscape.MenuSystem
{
    public class CustomGameMenuView : MenuView
    {
        [SerializeField] private InputField _widthField;

        [SerializeField] private InputField _heightField;

        [SerializeField] private InputField _exitsCountField;

        [SerializeField] private Button _playButton;

        public void Start()
        {
            _widthField.text = "15";
            _heightField.text = "15";
            _exitsCountField.text = "1";

            _widthField.onEndEdit.AddListener(ValidateWidth);
            _heightField.onEndEdit.AddListener(ValidateHeight);
            _exitsCountField.onEndEdit.AddListener(ValidateExitsCount);
        }

        private bool RefreshPlayButtonStatus()
        {
            if (string.IsNullOrEmpty(_widthField.text) ||
                string.IsNullOrEmpty(_heightField.text) ||
                string.IsNullOrEmpty(_exitsCountField.text))
            {
                _playButton.interactable = false;
                return false;
            }

            _playButton.interactable = true;

            return true;
        }

        private void ValidateExitsCount(string value)
        {
            if (!RefreshPlayButtonStatus())
                return;

            _exitsCountField.text = Mathf.Clamp(Convert.ToInt32(_exitsCountField.text), 1, 10).ToString();
        }

        private void ValidateWidth(string value)
        {
            if (!RefreshPlayButtonStatus())
                return;

            _widthField.text = Mathf.Clamp(Convert.ToInt32(_widthField.text), 3, 150).ToString();
        }

        private void ValidateHeight(string value)
        {
            if (!RefreshPlayButtonStatus())
                return;

            _heightField.text = Mathf.Clamp(Convert.ToInt32(_heightField.text), 3, 150).ToString();
        }

        public void OnPlayClicked()
        {
            SoundManagerView.Instance.PlayMenuChooseEffect();

            GameManager.Instance.CurrentGameType = GameType.Custom;
            GameManager.Instance.CustomGameFieldWidth = Convert.ToInt32(_widthField.text);
            GameManager.Instance.CustomGameFieldHeight = Convert.ToInt32(_heightField.text);
            GameManager.Instance.CustomGameExitsCount = Convert.ToInt32(_exitsCountField.text);
            SceneChanger.Instance.LoadGameScene();
        }
    }
}