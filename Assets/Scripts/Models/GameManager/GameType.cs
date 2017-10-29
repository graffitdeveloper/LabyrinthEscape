namespace LabyrinthEscape.GameManagerControls
{
    /// <summary>
    /// Тип запущенной игры
    /// </summary>
    public enum GameType
    {
        /// <summary>
        /// Легкая (15 на 15 клеток)
        /// </summary>
        Easy,

        /// <summary>
        /// Средняя (30 на 30 клеток)
        /// </summary>
        Medium,

        /// <summary>
        /// Сложная (50 на 50 клеток)
        /// </summary>
        Hard,

        /// <summary>
        /// Настраиваемая (от 3 до 150 клеток в ширину и высоту, + больше 1-10 выходов из лабиринта)
        /// </summary>
        Custom
    }
}