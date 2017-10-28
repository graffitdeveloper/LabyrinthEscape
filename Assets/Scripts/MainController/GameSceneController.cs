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

public class GameSceneController : MonoBehaviour
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
        _cameraView.SetToPlayer(_playerView.transform);

        LoaderView.Hide();
    }

    public void OnPlayerFinishedLabyrinth()
    {
        _playerView.OnPlayerFinishedLabyrinth = null;
        _cameraView.ReactToControls = false;

    }

    #endregion
}