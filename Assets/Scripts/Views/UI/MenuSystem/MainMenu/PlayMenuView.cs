using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.LabyrinthGeneratorControls;
using UnityEngine;

namespace LabyrinthEscape.MenuSystem
{
    public class PlayMenuView : MenuView
    {
        [SerializeField] private CustomGameMenuView _customGameMenuView;

        public void OnPlayEasyButtonClicked()
        {
            StartGame(GameType.Easy);
        }

        public void OnPlayMediumButtonClicked()
        {
            StartGame(GameType.Medium);
        }

        public void OnPlayHardButtonClicked()
        {
            StartGame(GameType.Hard);
        }

        private void StartGame(GameType gameType)
        {
            GameManager.Instance.CurrentGameType = gameType;
            SceneChanger.Instance.LoadGameScene();
        }

        public void OnPlayCustomButtonClicked()
        {
            _customGameMenuView.Show(this);
        }
    }
}