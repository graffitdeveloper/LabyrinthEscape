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
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                CurrentMovingDirection = InputDirection.Up;

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                CurrentMovingDirection = InputDirection.Right;

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                CurrentMovingDirection = InputDirection.Down;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                CurrentMovingDirection = InputDirection.Left;

            if (!Input.anyKey)
                CurrentMovingDirection = InputDirection.None;
        }
    }
}