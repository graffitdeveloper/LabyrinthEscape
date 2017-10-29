﻿using System;
using Assets.Scripts.LabyrinthElements;
using LabyrinthEscape.GameManagerControls;
using System.Collections;
using LabyrinthEscape.CameraControls;
using LabyrinthEscape.GridControls;
using LabyrinthEscape.LabyrinthGeneratorControls;
using LabyrinthEscape.Loader;
using LabyrinthEscape.PlayerControls;
using UnityEngine;

/// <summary>
/// Контроллер игровой сцены
/// </summary>
public class GameSceneController : MonoBehaviour
{
    #region Layout

#pragma warning disable 649

    /// <summary>
    /// Вьюшка лабиринта
    /// </summary>

    [SerializeField] private LabyrinthView _labyrinthView;

    /// <summary>
    /// Вьюшка камеры
    /// </summary>
    [SerializeField] private CameraView _cameraView;

    /// <summary>
    /// Вьюшка игрока
    /// </summary>
    [SerializeField] private PlayerView _playerView;

#pragma warning restore 649

    #endregion

    #region Methods

    /// <summary>
    /// Инициализация, сразу запускает генерацию лабиринта, т.к. это уже игровая сцена
    /// </summary>
    public void Start()
    {
        StartCoroutine(GenerateLabyrinth());
    }

    /// <summary>
    /// Генерация лабиринта в коротине
    /// </summary>
    public IEnumerator GenerateLabyrinth()
    {
        int gridWidth;
        int gridHeight;

        switch (GameManager.Instance.CurrentGameType)
        {
            case GameType.Easy:
                gridWidth = gridHeight = 15;
                break;

            case GameType.Medium:
                gridWidth = gridHeight = 30;
                break;

            case GameType.Hard:
                gridWidth = gridHeight = 50;
                break;

            case GameType.Custom:
                gridWidth = GameManager.Instance.CustomGameFieldWidth;
                gridHeight = GameManager.Instance.CustomGameFieldHeight;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        // Подстраивает размеры сетки под допустимые: для правильной постройки сетки ширина и высота сетки должны быть
        // с нечетной величиной, а так же больше или равны 3
        if (gridWidth < 3)
            gridWidth = 3;
        else if (gridWidth % 2 == 0)
            gridWidth--;

        if (gridHeight < 3)
            gridHeight = 3;
        else if (gridHeight % 2 == 0)
            gridHeight--;

        var labyrinth = new Grid();
        labyrinth.Init(gridWidth, gridHeight);

        LoaderView.SetProgress(0.2f);

        yield return StartCoroutine(LabyrinthGenerator.Instance.GenerateLabyrinth(labyrinth));

        LoaderView.SetProgress(1f);

        _labyrinthView.DrawGrid(labyrinth);

        _cameraView.ReactToControls = true;
        _playerView.Spawn(labyrinth.GetSpawnPoint());
        _playerView.OnPlayerFinishedLabyrinth += OnPlayerFinishedLabyrinth;
        _playerView.OnPlayerDoneStep += PlayerDoneStep;
        _cameraView.SetToPlayer(_playerView.transform);

        LoaderView.Hide();
    }

    /// <summary>
    /// Вызывается когда игрок перешагнул с клетки на клетку
    /// </summary>
    private void PlayerDoneStep()
    {
        GameManager.Instance.StepDone();
    }

    /// <summary>
    /// Вызывается когда игрок финишировал
    /// </summary>
    public void OnPlayerFinishedLabyrinth()
    {
        _playerView.OnPlayerFinishedLabyrinth = null;
        _cameraView.ReactToControls = false;
    }

    #endregion
}