using Assets.Scripts.Views;
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

            SoundManagerView.Instance.PlayMenuChooseEffect();
            GameManager.Instance.IsGamePaused = false;
        }

        public void OnYesButtonClicked()
        {
            SoundManagerView.Instance.PlayMenuChooseEffect();
            SceneChanger.Instance.LoadGameScene();
        }
    }
}