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
        var gridSizeX = 0;
        var gridSizeY = 0;

        switch (GameManager.Instance.CurrentGameType)
        {
            case GameType.Easy:
                gridSizeX = 15;
                gridSizeY = 15;
                break;

            case GameType.Medium:
                gridSizeX = 30;
                gridSizeY = 30;
                break;

            case GameType.Hard:
                gridSizeX = 50;
                gridSizeY = 50;
                break;

            case GameType.Custom:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
        
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

    }

    #endregion
}