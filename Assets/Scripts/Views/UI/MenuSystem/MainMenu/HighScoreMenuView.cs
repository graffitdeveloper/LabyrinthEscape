using System.Collections.Generic;
using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.HighScoreControls;
using UnityEngine;
using UnityEngine.UI;

namespace LabyrinthEscape.MenuSystem
{
    public class HighScoreMenuView : MenuView
    {
#pragma warning disable 649

        [SerializeField] private ScoreItem _scoreItemPrefab;

        [SerializeField] private Transform _scoreItemsContainer;

        [SerializeField] private Text _captionText;

#pragma warning restore 649

        private readonly List<ScoreItem> _scoreItemsList = new List<ScoreItem>();

        public void OnClearResultButtonPressed()
        {
            HighScoreData.ClearResults();

            OnEasyButtonPressed();
        }

        public void OnEasyButtonPressed()
        {
            FillList(GameType.Easy);
        }

        public void OnMediumButtonPressed()
        {
            FillList(GameType.Medium);
        }

        public void OnHardButtonPressed()
        {
            FillList(GameType.Hard);
        }

        public override void Show(MenuView previousMenu = null)
        {
            base.Show(previousMenu);

            OnEasyButtonPressed();
        }

        public override void Hide()
        {
            ClearList();

            base.Hide();
        }

        public void FillList(GameType gameType)
        {
            var highScore = HighScoreData.Load();

            switch (gameType)
            {
                case GameType.Easy:
                    FillList(highScore.EasyHighScore);
                    break;

                case GameType.Medium:
                    FillList(highScore.MediumHighScore);
                    break;

                case GameType.Hard:
                    FillList(highScore.HardHighScore);
                    break;
            }

            _captionText.text = string.Format("High score - {0}", gameType);
        }

        public void FillList(List<HighScoreItem> highScoreItems)
        {
            ClearList();

            foreach (var highScoreItem in highScoreItems)
            {
                var newItem = Instantiate(_scoreItemPrefab, _scoreItemsContainer);
                _scoreItemsList.Add(newItem);
                newItem.Init(highScoreItem, _scoreItemsList.Count);
            }
        }

        public void ClearList()
        {
            foreach (var labyrinthElement in _scoreItemsList)
                Destroy(labyrinthElement.gameObject);

            _scoreItemsList.Clear();
        }
    }
}