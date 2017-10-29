namespace LabyrinthEscape.GridControls
{
    /// <summary>
    /// Тип ячейки
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// Пустая ячейка, пол
        /// </summary>
        EmptyCell,

        /// <summary>
        /// Непроходимая стена
        /// </summary>
        Wall,

        /// <summary>
        /// Точка спавна игрока
        /// </summary>
        SpawnPoint,

        /// <summary>
        /// Точка финиша
        /// </summary>
        FinishPoint
    }
}