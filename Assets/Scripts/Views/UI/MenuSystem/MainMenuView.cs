namespace LabyrinthEscape.MenuSystem
{
    public class MainMenuView : MenuView
    {
        public PlayMenuView PlayMenu;
        public OptionsMenuView OptionsMenu;
        public HighScoreMenuView HighScoreMenu;
        public ExitMenuView ExitMenu;

        public void OnPlayButtonClicked()
        {
            PlayMenu.Show(this);
        }

        public void OnOptionsButtonClicked()
        {
            OptionsMenu.Show(this);
        }

        public void OnHighscoreButtonClicked()
        {
            HighScoreMenu.Show(this);
        }

        public void OnExitButtonClicked()
        {
            ExitMenu.Show(this);
        }
    }
}