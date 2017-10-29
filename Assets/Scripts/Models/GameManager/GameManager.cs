using LabyrinthEscape.PlayerControls;

namespace LabyrinthEscape.GameManagerControls
{
    /// <summary>
    /// Глобальный игровой менеджер, содержит разные важные для игры данные
    /// </summary>
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

        #region Fields

        /// <summary>
        /// Текущий выбранный тип игры
        /// </summary>
        public GameType CurrentGameType;

        /// <summary>
        /// Текущая выбранная ширина лабиринта, для кастомной игры
        /// </summary>
        public int CustomGameFieldWidth;

        /// <summary>
        /// Текущая выбранная высота лабиринта, для кастомной игры
        /// </summary>
        public int CustomGameFieldHeight;

        /// <summary>
        /// Текущее выбранное количество выходов из лабиринта, для кастомной игры
        /// </summary>
        public int CustomGameExitsCount;

        /// <summary>
        /// Начал-ли игрок бежать в игре?
        /// </summary>
        public bool IsGameStarted = false;

        /// <summary>
        /// Финишировал-ли игрок?
        /// </summary>
        public bool IsGameFinished = false;

        /// <summary>
        /// Находится-ли игра в состоянии паузы сейчас?
        /// </summary>
        public bool IsGamePaused = false;

        /// <summary>
        /// Текущее количество сделанных игроком шагов
        /// </summary>
        public int CurrentDoneSteps = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Отклик на сделанный игроком шаг
        /// </summary>
        public void StepDone()
        {
            CurrentDoneSteps++;
        }

        #endregion
    }
}