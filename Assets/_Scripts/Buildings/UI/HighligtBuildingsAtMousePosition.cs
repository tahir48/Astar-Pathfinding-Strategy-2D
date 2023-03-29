using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using StrategyGame_2DPlatformer.Contracts;

namespace StrategyGame_2DPlatformer.Buildings.UI
{
    public class HighligtBuildingsAtMousePosition : MonoBehaviour
    {
        /// <summary>
        /// When a building button is clicked on UI to plac eon tiles, this class highlights the tiles where the building can be placed or not.
        /// </summary>
        public Color unavalaibleColor;
        public Color availaibleColor;
        private List<Vector3Int> _currentTilePositions;
        private int sizeX;
        private int sizeY;
        private Vector3Int previousCellPosToCompare;
        private IPlaceable _placeable;

        private void Start()
        {
            _placeable = GetComponent<IPlaceable>();
            if (_placeable != null)
            {
                sizeX = _placeable.SizeX;
                sizeY = _placeable.SizeY;
            }
            _currentTilePositions = new List<Vector3Int>();
        }

        void Update()
        {
            // Get the current cell position under the mouse 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = GameData.instance.Tilemap.WorldToCell(mousePosition);

            if (tilePosition != previousCellPosToCompare) // Understand if the mouse position is changed.
            {

                // Reset the colors of the previously highlighted cells
                foreach (var pos in _currentTilePositions)
                {
                    if (!IsWithinBounds(pos, tilePosition))
                    {
                        ChangeTileColor(pos, Color.white);
                    }
                }

                int startX = tilePosition.x - (sizeX - 1) / 2;
                int startY = tilePosition.y - (sizeY - 1) / 2;

                // Get the positions of the cells around the current cell position
                _currentTilePositions.Clear();
                for (int x = startX; x < startX + sizeX; x++)
                {
                    for (int y = startY; y < startY + sizeY; y++)
                    {
                        Vector3Int position = new Vector3Int(x, y, tilePosition.z);
                        _currentTilePositions.Add(position);
                    }
                }

                // Highlight the cells that are not occupied
                foreach (var pos in _currentTilePositions)
                {
                    Node node = GameData.instance.Graph.GetNodeAtPosition(pos);
                    if (node != null && node.isOccupied)
                    {
                        ChangeTileColor(pos, unavalaibleColor);
                    }
                    else
                    {
                        ChangeTileColor(pos, availaibleColor);
                    }
                }
                previousCellPosToCompare = tilePosition;
            }
        }

        private void OnDisable()
        {
            if (_currentTilePositions != null)
            {
                foreach (var pos in _currentTilePositions)
                {
                    ChangeTileColor(pos, Color.white);
                }
            }
        }

        private bool IsWithinBounds(Vector3Int pos, Vector3Int centerPos)
        {
            int startX = centerPos.x - (sizeX - 1) / 2;
            int endX = startX + sizeX - 1;
            int startY = centerPos.y - (sizeY - 1) / 2;
            int endY = startY + sizeY - 1;
            return pos.x >= startX && pos.x <= endX && pos.y >= startY && pos.y <= endY;
        }

        private void ChangeTileColor(Vector3Int pos, Color color)
        {
            GameData.instance.Tilemap.SetTileFlags(pos, TileFlags.None);
            GameData.instance.Tilemap.SetColor(pos, color);
        }

    }
}
