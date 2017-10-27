using System.Collections;
using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.Loader;
using UnityEngine.SceneManagement;

namespace LabyrinthEscape.MenuSystem
{
    public class PlayMenuView : MenuView
    {
        public void OnPlayEasyButtonClicked()
        {
            StartGame(15, 15);
        }

        public void OnPlayMediumButtonClicked()
        {
            StartGame(30, 30);
        }

        public void OnPlayHardButtonClicked()
        {
            StartGame(50, 50);
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

            LoaderView.SetProgress(0.1f);
        }

        public void OnPlayCustomButtonClicked()
        {

        }
    }
}