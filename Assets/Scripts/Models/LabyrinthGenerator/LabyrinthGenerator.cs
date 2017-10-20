using LabyrinthEscape.GridControls;

namespace LabyrinthEscape.LabyrinthGeneratorControls
{
    public class LabyrinthGenerator
    {
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

        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        public Grid GenerateLabyrinth(int width, int height)
        {
            var resultLabyrinth = CreateEmptyLabyrinth(width, height);
            ModifyLabyrinthBlank(resultLabyrinth);
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
    }
}