using System.Collections;
using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LabyrinthEscape.MenuSystem
{
    public class PlayMenuView : MenuView
    {
        public void OnPlayEasyButtonClicked()
        {
            StartGame(30, 30);
        }

        public void OnPlayMediumButtonClicked()
        {
            StartGame(60, 60);
        }

        public void OnPlayHardButtonClicked()
        {
            StartGame(90, 90);
        }

        private void StartGame(int chosenGridSizeX, int chosenGridSizeY)
        {
            GameManager.Instance.ChosenGridSizeX = chosenGridSizeX;
            GameManager.Instance.ChosenGridSizeY = chosenGridSizeY;

            LoaderView.Show();
            LoaderView.SetProgress(0f);

            StartCoroutine(LoadSceneAsync());
        }

        IEnumerator LoadSceneAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("Game");

            while (!asyncLoad.isDone)
                yield return null;

            LoaderView.SetProgress(0.2f);
        }

        public void OnPlayCustomButtonClicked()
        {

        }
    }
}