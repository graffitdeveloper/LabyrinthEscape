using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.LabyrinthGeneratorControls;

namespace LabyrinthEscape.MenuSystem
{
    class RetryMenu : MenuView
    {
        public override void ShowWithoutParamethers()
        {
            base.ShowWithoutParamethers();

            GameManager.Instance.IsGamePaused = true;
        }

        public override void OnBackButtonClicked()
        {
            base.OnBackButtonClicked();

            GameManager.Instance.IsGamePaused = false;
        }

        public void OnYesButtonClicked()
        {
            SceneChanger.Instance.LoadGameScene();
        }
    }
}