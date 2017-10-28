using LabyrinthEscape.HighScoreControls;
using LabyrinthEscape.Loader;
using LabyrinthEscape.MenuSystem;
using UnityEngine;

namespace LabyrinthEscape.MainMenuControls
{
    public class MainSceneController : MonoBehaviour
    {
        [SerializeField]
        private MainMenuView _mainMenu;

        [SerializeField]
        private Animation _titleAnimation;

        [SerializeField]
        private GameObject _pressAnyKeyLabel;

        private bool _isTitleHided;

        public void Start()
        {
            _mainMenu.Hide();
            LoaderView.Hide();
            _titleAnimation.Play("LabyrinthEscape_Tittle_Show");
            _titleAnimation.CrossFadeQueued("LabyrinthEscape_Tittle_Loop");
            _pressAnyKeyLabel.gameObject.SetActive(true);
            _isTitleHided = false;
        }

        public void Update()
        {
            if (!_isTitleHided)
            {
                if (Input.anyKeyDown)
                {
                    _isTitleHided = true;
                    _mainMenu.Show();
                    _titleAnimation.Play("LabyrinthEscape_Tittle_Small");
                    _pressAnyKeyLabel.gameObject.SetActive(false);
                }
            }
        }
    }
}