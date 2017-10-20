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
            // список всех свободных ячеек
            var freeCells = grid.GetFreeCells();

            // список всех единожды посещенных свободных ячеек
            var visitedCells = new List<GridCell>();

            // стек-путь генерации ячеек
            var path = new Stack<GridCell>();

            // стартовую точку берем рандомно, и отмечаем её как посещенную и текущую, а так же добавляем её в
            // пройденный путь
            var currentCell = freeCells[Random.Range(0, freeCells.Count)];
            visitedCells.Add(currentCell);
            path.Push(currentCell);

            for (int i = 0; i < 1000; i++)
            {
                // получаем все существующие соседние ячейки от текущей
                var neighborsDirections = currentCell.GetNeighborsDirections();

                while (true)
                {
                    // выбираем рандомно одну из возможных сторон направления к соседней ячейке
                    int randomDirectionIndex = Random.Range(0, neighborsDirections.Count);

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
                        var nearbyCell = currentCell.GetNearbyCell((Direction) randomDirectionIndex);
                        nearbyCell.CellType = CellType.EmptyCell;

                        visitedCells.Add(nearbyCell);
                        freeCells.Add(nearbyCell);
                        visitedCells.Add(randomNeighborCell);
                        path.Push(nearbyCell);
                        path.Push(randomNeighborCell);
                        currentCell = randomNeighborCell;

                        break;
                    }
                }

                if (freeCells.Count == visitedCells.Count)
                    return;
            }
        }
    }
}