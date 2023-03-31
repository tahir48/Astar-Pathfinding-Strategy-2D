using StrategyGame_2DPlatformer.Core;
using StrategyGame_2DPlatformer.GameManagement;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class PlacementPositionHandler : MonoBehaviour
    {
        /// <summary>
        /// This class is responsible for deciding if the building can be placed on the map.
        /// And if it can be placed, it assigns the positions of the nodes that the building will occupy.
        /// </summary>
        private List<Vector3Int> currentTilePositions;
        private List<Vector3Int> _positionsToPlace;
        public List<Vector3Int> PositionsToPlace { get { return _positionsToPlace; } private set { } }
        private int sizeX;
        private int sizeY;
        private IPlaceable _placeable;

        void Start()
        {
            _placeable = GetComponent<IPlaceable>();
            sizeX = _placeable.SizeX;
            sizeY = _placeable.SizeY;
            currentTilePositions = new List<Vector3Int>();
        }

        // Update is called once per frame
        void Update()
        {
            // Find TileMap position of the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = GameData.instance.Tilemap.WorldToCell(mousePosition);

            // Pick a start value of the positions in each direction so that mouse position will be on the center
            int startX = tilePosition.x - (sizeX - 1) / 2;
            int startY = tilePosition.y - (sizeY - 1) / 2;

            currentTilePositions.Clear();
            GetTilePositionsAroundMousePosition(tilePosition, startX, startY);
            int occupiedCount = 0;
            if (currentTilePositions.Count != 0)
            {
                occupiedCount = GetNumberOfOccupiedNodes(occupiedCount);
                DecideIfTheLocationIsPlaceable(occupiedCount);
            }
            AssignPlaceablePositionsIfExists(occupiedCount);
        }

        private void GetTilePositionsAroundMousePosition(Vector3Int tilePosition, int startX, int startY)
        {
            for (int x = startX; x < startX + sizeX; x++)
            {
                for (int y = startY; y < startY + sizeY; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, tilePosition.z);
                    currentTilePositions.Add(position);
                }
            }
        }
        private int GetNumberOfOccupiedNodes(int occupiedCount)
        {
            foreach (var item in currentTilePositions)
            {
                //Understand # of nodes that are occupied
                bool occupied = false;
                if (GameData.instance.Graph.GetNodeAtPosition(item) != null)
                {
                    occupied = GameData.instance.Graph.GetNodeAtPosition(item).isOccupied;
                }
                if (occupied) occupiedCount++;
            }

            return occupiedCount;
        }
        private void DecideIfTheLocationIsPlaceable(int occupiedCount)
        {
            if (occupiedCount == 0)
            {
                //if none of the nodes are occupied, we assume the building is placeable to that location
                _placeable.IsPlaceable = true;
            }
            else
            {
                // If any tile under the building is occupied, we assume this position to be unavailaible
                _placeable.IsPlaceable = false;
            }
        }
        private void AssignPlaceablePositionsIfExists(int occupiedCount)
        {
            if (occupiedCount == 0)
            {
                _positionsToPlace = currentTilePositions;
            }
            else
            {
                _positionsToPlace?.Clear();
            }
        }
    }
}
