using System.Collections.Generic;
using LabyrinthEscape.GridControls;
using UnityEngine;

namespace LabyrinthEscape.LabyrinthGeneratorControls
{
    public class LabyrinthGenerator
    {
        #region Singleton

        private LabyrinthGenerator()
        {
        }

        private static LabyrinthGenerator _instance;

        public static LabyrinthGenerator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LabyrinthGenerator();

                return _instance;
            }
        }

        #endregion

        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        public Grid GenerateLabyrinth(int width, int height)
        {
            var resultLabyrinth = CreateEmptyLabyrinth(width, height);
            ModifyLabyrinthBlank(resultLabyrinth);
            ModifyLabyrinthCreateRandomCoridors(resultLabyrinth);
            return resultLabyrinth;
        }

        /// <summary>
        /// Создает пустое поле из пола
        /// </summary>
        /// <param name="width">Ширина поля</param>
        /// <param name="height">Высота поля</param>
        private Grid CreateEmptyLabyrinth(int width, int height)
        {
            var grid = new Grid();
            grid.Init(width, height);
            return grid;
        }

        /// <summary>
        /// Генерирует заготовку лабиринта
        /// </summary>
        private void ModifyLabyrinthBlank(Grid grid)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    // пробегаемся по всем ячейкам, заполняя то все ячейки по x, то через одну, стеной, для того, что бы 
                    // подготовить заготовку для лабиринта
                    if (y % 2 != 0 && x % 2 != 0)
                        x++;

                    grid.SetCellStatus(x, y, CellType.Wall);
                }
            }
        }

        private void ModifyLabyrinthCreateRandomCoridors(Grid grid)
        {
            var freeCells = grid.GetFreeCells();
            var visitedCells = new List<GridCell>();
            var path = new Stack<GridCell>();

            // стартовую точку берем рандомно, и отмечаем её как посещенную
            var currentCell = freeCells[Random.Range(0, freeCells.Count - 1)];
            visitedCells.Add(currentCell);
            path.Push(currentCell);

            for (int i = 0; i < 100; i++)
            {
                var neighborsDirections = currentCell.GetNeighborsDirections();

                while (true)
                {
                    int randomDirectionIndex = Random.Range(0, neighborsDirections.Count - 1);

                    var randomNeighborCell =
                        currentCell.GetNeighbourCell(neighborsDirections[randomDirectionIndex]);

                    if (visitedCells.Contains(randomNeighborCell))
                    {
                        neighborsDirections.RemoveAt(randomDirectionIndex);
                        if (neighborsDirections.Count == 0)
                        {
                            currentCell = path.Pop();
                            break;
                        }
                    }
                    else
                    {
                        currentCell = randomNeighborCell;
                        visitedCells.Add(currentCell);
                        path.Push(currentCell);
                        currentCell.GetNearbyCell((Direction) randomDirectionIndex).CellType = CellType.EmptyCell;

                        break;
                    }
                }

                if (freeCells.Count == visitedCells.Count)
                    return;
            }
        }
    }
}