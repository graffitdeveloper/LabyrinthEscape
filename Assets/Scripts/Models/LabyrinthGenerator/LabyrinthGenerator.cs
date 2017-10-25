using System.Collections;
using System.Collections.Generic;
using LabyrinthEscape.GridControls;
using LabyrinthEscape.Loader;
using UnityEngine;

namespace LabyrinthEscape.LabyrinthGeneratorControls
{
    public class LabyrinthGenerator : MonoBehaviour
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
                {
                    var gameObject = new GameObject("LabyrinthGenerator");
                    _instance = gameObject.AddComponent<LabyrinthGenerator>();
                }

                return _instance;
            }
        }

        #endregion

        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="resultLabyrinth"></param>
        public IEnumerator GenerateLabyrinth(Grid resultLabyrinth)
        {
            yield return StartCoroutine(ModifyLabyrinthBlank(resultLabyrinth));
            LoaderView.SetProgress(0.3f);

            yield return StartCoroutine(ModifyLabyrinthCreateRandomCoridors(resultLabyrinth));
        }

        /// <summary>
        /// Генерирует заготовку лабиринта
        /// </summary>
        private IEnumerator ModifyLabyrinthBlank(Grid grid)
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

            // для того что бы отрисовать прогресс
            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// Расчет итогового количества свободных ячеек в готовом лабиринте.
        /// Количество свободных в итоге ячеек определяет не позиция ячеек, а размер сетки, поэтому мы можем при помощи
        /// формулы расчитать, сколько ячеек будет в итоге свободными, зная только о размерах будущего лабиринта. Это
        /// Полезно при отображении прогресса генерации
        /// </summary>
        /// <param name="grid">Сетка, из которой будет генерироваться лабиринт</param>
        private int GetFreeCellsCount(Grid grid)
        {
            int countPossibleCellsInRow = grid.Width - 2;
            int countPossibleRows = (grid.Height - 1) / 2;
            int countTunnelsBetweenRows = countPossibleRows - 1;

            return countPossibleCellsInRow * countPossibleRows + countTunnelsBetweenRows;
        }

        private IEnumerator ModifyLabyrinthCreateRandomCoridors(Grid grid)
        {
            var freeCellsInReadyLabyrinthCount = GetFreeCellsCount(grid);
            var yieldsCount = 15;
            var currentYield = 0;

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
                    if (currentYield < yieldsCount)
                    {
                        var percentageDoneVisitedCells = visitedCells.Count / (float) freeCellsInReadyLabyrinthCount;
                        var percentageDoneYields = currentYield / (float) yieldsCount;

                        if (percentageDoneVisitedCells > percentageDoneYields)
                        {
                            currentYield++;
                            LoaderView.SetProgress(0.2f + 0.8f * visitedCells.Count / freeCellsInReadyLabyrinthCount);
                            yield return null;
                        }
                    }

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