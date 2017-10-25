using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Позиция ячейки по X
        /// </summary>
        public int PositionX { get; private set; }

        /// <summary>
        /// Позиция ячейки по Y
        /// </summary>
        public int PositionY { get; private set; }

        private Grid _parentGrid;

        #endregion

        #region Methods

        /// <summary>
        /// Конструктор ячейки
        /// </summary>
        /// <param name="parentGrid">Сетка, которой принадлежит клетка</param>
        /// <param name="cellType">Тип, которым мы инициализируем ячейку</param>
        /// <param name="positionX">Позиция ячейки по X</param>
        /// <param name="positionY">Позиция ячейки по Y</param>
        public GridCell(Grid parentGrid, CellType cellType, int positionX, int positionY)
        {
            _parentGrid = parentGrid;
            CellType = cellType;
            PositionX = positionX;
            PositionY = positionY;
        }

        /// <summary>
        /// Возвращает прилегающую клетку. Прилегающая - значит та, что находится непосредственно рядом с текущей, не
        /// путать с соседней
        /// </summary>
        /// <param name="cellDirection">Направление, в котором ищем ячейку</param>
        public GridCell GetNearbyCell(CellDirection cellDirection)
        {
            switch (cellDirection)
            {
                case CellDirection.Up:
                    return _parentGrid.GetCell(PositionX, PositionY + 1);
                case CellDirection.Right:
                    return _parentGrid.GetCell(PositionX + 1, PositionY);
                case CellDirection.Down:
                    return _parentGrid.GetCell(PositionX, PositionY - 1);
                case CellDirection.Left:
                    return _parentGrid.GetCell(PositionX - 1, PositionY);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Возвращает соседнюю ячейку. Соседняя - значит та, что через стенку от текущей, не путать с прилегающей к
        /// текущей. Если ячейку в данном направлении нет - возвращает null
        /// <param name="cellDirection">Направление, в котором ищем ячейку</param>
        /// </summary>
        public GridCell GetNeighbourCell(CellDirection cellDirection)
        {
            switch (cellDirection)
            {
                case CellDirection.Up:
                    return _parentGrid.GetCell(PositionX, PositionY + 2);
                case CellDirection.Right:
                    return _parentGrid.GetCell(PositionX + 2, PositionY);
                case CellDirection.Down:
                    return _parentGrid.GetCell(PositionX, PositionY - 2);
                case CellDirection.Left:
                    return _parentGrid.GetCell(PositionX - 2, PositionY);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Возвращает направления всех соседних ячеек. Соседняя - значит та, что через стенку от текущей, не путать с
        /// прилегающей к текущей. 
        /// </summary>
        public List<CellDirection> GetNeighborsDirections()
        {
            var result = new List<CellDirection>();

            for (int i = 0; i < Enum.GetNames(typeof(CellDirection)).Length; i++)
                if (GetNeighbourCell((CellDirection) i) != null)
                    result.Add((CellDirection) i);

            return result;
        }

        #endregion
    }
}