using UnityEngine;

namespace LabyrinthEscape.MenuSystem
{
    public class MenuView : MonoBehaviour
    {
        private MenuView _previousMenu;

        /// <summary>
        /// Отображает меню. Меню, указанное параметром будет считаться
        /// предыдущим, если же ничего не указывать, будет использоваться
        /// последнее переданное. Если и такого нету, коллбэк на пункт back
        /// просто скроет текущее меню
        /// </summary>
        /// <param name="previousMenu">Меню, которое будет считаться
        /// предыдущим. Если же ничего не указывать, будет использоваться
        /// последнее переданное. Если и такого нету, коллбэк на пункт back
        /// просто скроет текущее меню</param>
        public void Show(MenuView previousMenu = null)
        {
            if (previousMenu != null)
            {
                _previousMenu = previousMenu;
                _previousMenu.Hide();
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnBackButtonClicked()
        {
            if (_previousMenu != null)
                _previousMenu.Show();

            Hide();
        }
    }
}