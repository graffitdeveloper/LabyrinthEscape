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

        [SerializeField] private Button _playButton;

        public void Start()
        {
            _widthField.text = "15";
            _heightField.text = "15";

            _widthField.onEndEdit.AddListener(ValidateWidth);
            _heightField.onEndEdit.AddListener(ValidateHeight);
        }

        private void ValidateWidth(string value)
        {
            if (string.IsNullOrEmpty(_widthField.text) ||
                string.IsNullOrEmpty(_heightField.text))
            {
                _playButton.interactable = false;
                return;
            }

            _widthField.text = Mathf.Clamp(Convert.ToInt32(_widthField.text), 3, 150).ToString();
            _playButton.interactable = true;
        }

        private void ValidateHeight(string value)
        {
            if (string.IsNullOrEmpty(_widthField.text) ||
                string.IsNullOrEmpty(_heightField.text))
            {
                _playButton.interactable = false;
                return;
            }

            _heightField.text = Mathf.Clamp(Convert.ToInt32(_heightField.text), 3, 150).ToString();
            _playButton.interactable = true;
        }

        public void OnPlayClicked()
        {
            SoundManagerView.Instance.PlayMenuChooseEffect();

            GameManager.Instance.CurrentGameType = GameType.Custom;
            GameManager.Instance.CustomGameFieldWidth = Convert.ToInt32(_widthField.text);
            GameManager.Instance.CustomGameFieldHeight = Convert.ToInt32(_heightField.text);
            SceneChanger.Instance.LoadGameScene();
        }
    }
}