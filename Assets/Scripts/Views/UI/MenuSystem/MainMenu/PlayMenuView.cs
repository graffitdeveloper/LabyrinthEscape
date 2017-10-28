using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.LabyrinthGeneratorControls;

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

            SceneChanger.Instance.LoadGameScene();
        }

        public void OnPlayCustomButtonClicked()
        {

        }
    }
}