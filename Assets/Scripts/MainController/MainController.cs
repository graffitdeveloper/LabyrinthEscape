﻿using Assets.Scripts.LabyrinthElements;
using LabyrinthEscape.GameManagerControls;
using System.Collections;
using LabyrinthEscape.CameraControls;
using LabyrinthEscape.GridControls;
using LabyrinthEscape.LabyrinthGeneratorControls;
using LabyrinthEscape.Loader;
using LabyrinthEscape.PlayerControls;
using UnityEngine;

public class MainController : MonoBehaviour
{
    #region Layout

    [SerializeField] private LabyrinthView _labyrinthView;

    [SerializeField] private CameraView _cameraView;

    [SerializeField] private PlayerView _playerView;

    #endregion

    #region Methods

    public void Start()
    {
        StartCoroutine(GenerateLabyrinth());
    }

    public IEnumerator GenerateLabyrinth()
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


        var labyrinth = new Grid();
        labyrinth.Init(gridSizeX, gridSizeY);

        LoaderView.SetProgress(0.2f);

        yield return StartCoroutine(LabyrinthGenerator.Instance.GenerateLabyrinth(labyrinth));

        LoaderView.SetProgress(1f);
        _labyrinthView.DrawGrid(labyrinth);

        _cameraView.ReactToControls = true;
        _playerView.Spawn(labyrinth.GetSpawnPoint());
        _playerView.OnPlayerFinishedLabyrinth += OnPlayerFinishedLabyrinth;
        _cameraView.SetToPlayer(_playerView.transform);

        LoaderView.Hide();
    }

    public void OnPlayerFinishedLabyrinth()
    {
        _playerView.OnPlayerFinishedLabyrinth = null;
        _cameraView.ReactToControls = false;

        //todo

    }

    #endregion
}