using LabyrinthEscape.Loader;

namespace LabyrinthEscape.GameManagerControls
{
    public class GameManager
    {
        #region Singleton

        private GameManager()
        {
        }

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();

                return _instance;
            }
        }

        #endregion

        public int ChosenGridSizeX;

        public int ChosenGridSizeY;

        public bool IsGameStarted = false;

        public bool IsGameFinished = false;

        public bool IsGamePaused = false;
    }
}