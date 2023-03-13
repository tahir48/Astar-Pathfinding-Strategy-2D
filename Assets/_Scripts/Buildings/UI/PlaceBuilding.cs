using StrategyGame_2DPlatformer.GameManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class PlaceBuilding : MonoBehaviour
    {

        private List<Vector3Int> currentTilePositions; // Will depend on mouse position and building size when the class will be over
        private List<Vector3Int> _positionsToPlace;
        public List<Vector3Int> PositionsToPlace { get { return _positionsToPlace; } private set { } }
        private int sizeX;
        private int sizeY;


        void Start()
        {
            sizeX = 4;
            sizeY = 4;
            currentTilePositions = new List<Vector3Int>();
        }

        // Update is called once per frame
        void Update()
        {
            currentTilePositions.Clear();

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = GameData.instance.Tilemap.WorldToCell(mousePosition);

            for (int x = tilePosition.x - sizeX / 2; x <= tilePosition.x + sizeX / 2; x++)
            {
                for (int y = tilePosition.y - sizeY / 2; y <= tilePosition.y + sizeY / 2; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, tilePosition.z);
                    currentTilePositions.Add(position);
                }
            }

            int occupiedCount = 0;
            if (currentTilePositions.Count != 0)
            {
                foreach (var item in currentTilePositions)
                {
                    bool occupied = false;
                    if (GameData.instance.Graph.GetNodeAtPosition(item) != null)
                    {
                        occupied = GameData.instance.Graph.GetNodeAtPosition(item).isOccupied;
                    }
                    if (occupied) { occupiedCount++; }
                }
            }


            // If any tile under the building is occupied, we count this position to be unavailaible
            if (occupiedCount == 0)
            {
                _positionsToPlace = currentTilePositions;
            }
            else
            {
                _positionsToPlace.Clear();
            }
        }

    }
}
