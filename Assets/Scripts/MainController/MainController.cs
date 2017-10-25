using Assets.Scripts.LabyrinthElements;
using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.LabyrinthGeneratorControls;
using LabyrinthEscape.Loader;
using UnityEngine;

public class MainController : MonoBehaviour
{
    #region Layout

    [SerializeField] private LabyrinthView _labyrinthView;

    #endregion

    #region Methods

    public void Start()
    {
        int gridSizeX = GameManager.Instance.ChosenGridSizeX;
        int gridSizeY = GameManager.Instance.ChosenGridSizeY;

        // Подстраивает размеры сетки под допустимые: для правильной постройки сетки ширина и высота сетки должны быть
        // с нечетной величиной, а так же больше или равны 3
        if (gridSizeX < 3)
            gridSizeX = 3;
        else if (gridSizeX % 2 == 0)
            gridSizeX--;

        if (gridSizeY < 3)
            gridSizeY = 3;
        else if (gridSizeY % 2 == 0)
            gridSizeY--;

        var labyrinth = LabyrinthGenerator.Instance.GenerateLabyrinth(gridSizeX, gridSizeY);

        LoaderView.SetProgress(0.7f);

        _labyrinthView.DrawGrid(labyrinth);

        LoaderView.SetProgress(1f);
        LoaderView.Hide();
    }

    #endregion
}