using UnityEngine;

namespace LabyrinthEscape.InputControls
{
    public class InputController : MonoBehaviour
    {
        #region Singleton

        private InputController()
        {
        }

        private static InputController _instance;

        public static InputController Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject("InputController");
                    _instance = gameObject.AddComponent<InputController>();
                }

                return _instance;
            }
        }

        #endregion

        private InputDirection _currentMovingDirection = InputDirection.None;

        public InputDirection CurrentMovingDirection
        {
            get { return _currentMovingDirection; }
            private set { _currentMovingDirection = value; }
        }

        public void Update()
        {
            CheckDirectionsInput();
        }

        private void CheckDirectionsInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                CurrentMovingDirection = InputDirection.Up;

            if (Input.GetKeyDown(KeyCode.RightArrow))
                CurrentMovingDirection = InputDirection.Right;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                CurrentMovingDirection = InputDirection.Down;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                CurrentMovingDirection = InputDirection.Left;

            if (!Input.anyKey)
                CurrentMovingDirection = InputDirection.None;
        }
    }
}