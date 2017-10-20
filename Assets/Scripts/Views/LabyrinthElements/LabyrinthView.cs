﻿using System.Collections.Generic;
using LabyrinthEscape.LabyrinthElements;
using UnityEngine;
using LabyrinthEscape.GridControls;

namespace Assets.Scripts.LabyrinthElements
{
    public class LabyrinthView : MonoBehaviour
    {
        #region Layout

        [SerializeField] private WallView _wallViewPrefab;

        [SerializeField] private FloorView _floorViewPrefab;

        [SerializeField] private Transform _labyrinthElementsContainer;

        #endregion

        #region Fields

        private List<LabyrinthElementView> _labyrinthElements = new List<LabyrinthElementView>();

        #endregion

        #region Methods

        public void DrawGrid(Grid grid)
        {
            foreach (var labyrinthElement in _labyrinthElements)
                Destroy(labyrinthElement.gameObject);
            
            _labyrinthElements = new List<LabyrinthElementView>();

            for (int y = 0; y < grid.Height; y++)
            for (int x = 0; x < grid.Width; x++)
            {
                // Здесь инстанциируются префабы
                switch (grid.GetCell(x, y).CellType)
                {
                    case CellType.EmptyCell:
                        var newFloor = Instantiate(_floorViewPrefab, _labyrinthElementsContainer);
                        _labyrinthElements.Add(newFloor);
                        newFloor.Position = new Vector3(x, y);
                        break;

                    case CellType.Wall:
                        var newWall = Instantiate(_wallViewPrefab, _labyrinthElementsContainer);
                        _labyrinthElements.Add(newWall);
                        newWall.Position = new Vector3(x, y);
                        break;

                    case CellType.SpawnPoint:

                        break;

                    case CellType.FinishPoint:

                        break;
                }
            }

            grid = null;
        }

        #endregion
    }
}