namespace LabyrinthEscape.GridControls
{
    /// <summary>
    /// Класс ячейки
    /// </summary>
    public class GridCell
    {
        #region Fields

        /// <summary>
        /// Тип ячейки
        /// </summary>
        public CellType CellType;

        #endregion

        #region Methods

        /// <summary>
        /// Конструктор ячейки
        /// </summary>
        /// <param name="cellType">Тип, которым мы инициализируем ячейку</param>
        public GridCell(CellType cellType)
        {
            CellType = cellType;
        }

        #endregion
    }
}