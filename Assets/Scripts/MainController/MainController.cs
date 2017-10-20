using Assets.Scripts.LabyrinthElements;
using LabGen.LabyrinthGeneratorControls;
using UnityEngine;

public class MainController : MonoBehaviour
{
    #region Layout

    [SerializeField] private int _labWidth;

    [SerializeField] private int _labHeight;

    [SerializeField] private LabyrinthView _labyrinthView;

    #endregion

    #region Methods

    /// <summary>
    /// Initialization
    /// </summary>
    public void Start()
    {
        // Подстраивает размеры сетки под допустимые: для правильной постройки сетки ширина и высота сетки должны быть
        // с нечетной величиной, а так же больше или равны 3
        if (_labWidth < 3)
            _labWidth = 3;
        else if (_labWidth % 2 == 0)
            _labWidth--;

        if (_labHeight < 3)
            _labHeight = 3;
        else if (_labHeight % 2 == 0)
            _labHeight--;

        var labyrinth = LabyrinthGenerator.Instance.GenerateLabyrinth(_labWidth, _labHeight);

        _labyrinthView.DrawGrid(labyrinth);
    }

    #endregion
}