using System.Collections.Generic;
using LabyrinthEscape.GridControls;
using UnityEngine;
using UnityEngine.Profiling;

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

            // список всех хотя-бы раз посещенных свободных ячеек
            var visitedCells = new List<GridCell>();

            // стек-путь генерации ячеек (необходим для того, что бы если генерация зайдет в тупик - можно было
            // вернуться назад, и добавить все возможные коридоры
            var path = new Stack<GridCell>();

            // стартовую ячейку берем рандомно из свободных, и отмечаем её как посещенную и текущую, а так же добавляем
            // её в пройденный путь
            var currentCell = freeCells[Random.Range(0, freeCells.Count)];
            visitedCells.Add(currentCell);
            path.Push(currentCell);

            // запуск генерации коридоров. Будет происходить до тех пор, пока алгоритм не пройдется по всем свободным
            // клеткам
            do
            {
                // все стороны, в которых есть свободные ячейки
                var neighborsDirections = currentCell.GetNeighborsDirections();

                // если нет направлений - это лабиринт из одной пустой ячейки, лабиринт готов
                if (neighborsDirections.Count == 0)
                    break;

                while (true)
                {
                    // выбираем рандомно одну из возможных сторон направления к соседней ячейке
                    int randomDirectionIndex = Random.Range(0, neighborsDirections.Count);

                    var randomNeighborCell =
                        currentCell.GetNeighbourCell(neighborsDirections[randomDirectionIndex]);
                    var randomNearbyCell =
                        currentCell.GetNearbyCell(neighborsDirections[randomDirectionIndex]);

                    // направление нам не подходит если:
                    // 1. мы его уже посещали
                    // 2. за прилегающей ячейкой - стена (нам ведь не хочется перепрыгнуть на стену)
                    // 3. прилегающая ячейка - не стена (алгоритм ломает стены, а не ходит по пустому пространству)
                    if (visitedCells.Contains(randomNeighborCell)
                        || randomNeighborCell.CellType == CellType.Wall
                        || randomNearbyCell.CellType == CellType.EmptyCell)
                    {
                        // удаляем это направление из списка возможных, и пробуем другие направления
                        neighborsDirections.RemoveAt(randomDirectionIndex);

                        // если допустимых направлений не осталось - возвращаемся назад по стеку, проверяя попутно те
                        // стены, которые мы пропустили раньше в силу выбора другого направления
                        if (neighborsDirections.Count == 0)
                        {
                            currentCell = path.Pop();
                            break;
                        }
                    }
                    else
                    {
                        // направление нам подошло, ломаем стену, отмечаем бывшую стену, и следующую за ней ячейку как
                        // посещенные и добавляем их в пройденный путь
                        randomNearbyCell.CellType = CellType.EmptyCell;
                        visitedCells.Add(randomNearbyCell);
                        freeCells.Add(randomNearbyCell);
                        visitedCells.Add(randomNeighborCell);
                        path.Push(randomNearbyCell);
                        path.Push(randomNeighborCell);
                        currentCell = randomNeighborCell;

                        break;
                    }
                }
                // Когда количество свободных клеток совпадет с количеством посещенных - лабиринт готов
            } while (freeCells.Count != visitedCells.Count);
        }
    }
}