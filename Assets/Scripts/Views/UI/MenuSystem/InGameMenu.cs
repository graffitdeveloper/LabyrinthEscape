using LabyrinthEscape.GameManagerControls;
using UnityEngine;

namespace LabyrinthEscape.MenuSystem
{
    class InGameMenu : MenuView
    {
        public OptionsMenuView OptionsMenu;
        public ExitMenuView ExitMenu;
        public GameObject RootGameObject;

        public void OnOptionsButtonClicked()
        {
            OptionsMenu.Show(this);
        }

        public void OnExitButtonClicked()
        {
            ExitMenu.Show(this);
        }

        public override void ShowWithoutParamethers()
        {
            base.ShowWithoutParamethers();

            RootGameObject.SetActive(true);
            GameManager.Instance.IsGamePaused = true;
        }

        public override void OnBackButtonClicked()
        {
            base.OnBackButtonClicked();

            GameManager.Instance.IsGamePaused = false;
            RootGameObject.SetActive(false);
        }
    }
}