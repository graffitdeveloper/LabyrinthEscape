using UnityEngine;

namespace LabyrinthEscape.MenuSystem
{
    public class ExitMenuView : MenuView
    {
        public void OnYesClicked()
        {
            Application.Quit();
        }
    }
}