using LabyrinthEscape.LabyrinthGeneratorControls;
using UnityEngine;

namespace LabyrinthEscape.MenuSystem
{
    public class ExitMenuView : MenuView
    {
        [SerializeField] private bool _isExitToMainScreen;

        public void OnYesClicked()
        {
            if (_isExitToMainScreen)
            {
                SceneChanger.Instance.LoadMainScene();
            }
            else
            {
                Application.Quit();
            }
        }
    }
}