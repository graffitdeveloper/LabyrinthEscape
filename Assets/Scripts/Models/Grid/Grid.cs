using System.Collections.Generic;

namespace LabyrinthEscape.GridControls
{
    /// <summary>
    /// Класс сетки
    /// </summary>
    public class Grid
    {
        #region Fields

        /// <summary>
        /// Ширина сетки
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Высота сетки
        /// </summary>
        public int Height { get; private set; }

        public int CellsCount { get; private set; }

        /// <summary>
        /// Массив всех ячеек
        /// </summary>
        private GridCell[,] _gridCells;

        #endregion

        #region Methods

        /// <summary>
        /// Инициализация сетки. Сетка создается с указанными размерами, все клетки заполняются значением
        /// <see cref="CellType.EmptyCell"/>
        /// </summary>
        /// <param name="width">Ширина сетки</param>
        /// <param name="height">Высота сетки</param>
        public void Init(int width, int height)
        {
            Width = width;
            Height = height;

            _gridCells = new GridCell[Width, Height];

            for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                _gridCells[x, y] = new GridCell(this, CellType.EmptyCell, x, y);
        }

        /// <summary>
        /// Установка нового состояния для ячейки
        /// </summary>
        /// <param name="x">Позиция целевой ячейки по x</param>
        /// <param name="y">Позиция целевой ячейки по y</param>
        /// <param name="newStatus">Новое состояние для ячейки</param>
        public void SetCellStatus(int x, int y, CellType newStatus)
        {
            if (x >= Width || y >= Height)
                throw new System.Exception(
                    string.Format("Sorry, but cell is out of range! x:{0}, y:{1}, width:{2}, height:{3}", x, y, Width,
                        Height));

            _gridCells[x, y].CellType = newStatus;
        }

        /// <summary>
        /// Возвращает состояние ячейки
        /// </summary>
        /// <param name="x">Позиция целевой ячейки по x</param>
        /// <param name="y">Позиция целевой ячейки по y</param>
        public GridCell GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return null;

            return _gridCells[x, y];
        }

        #endregion

        public List<GridCell> GetFreeCells()
        {
            var result = new List<GridCell>();

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    if (_gridCells[x, y].CellType == CellType.EmptyCell)
                        result.Add(_gridCells[x, y]);

            return result;
        }

        public GridCell GetSpawnPoint()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (_gridCells[x, y].CellType == CellType.SpawnPoint)
                        return _gridCells[x, y];
                }

            return null;
        }
    }
}