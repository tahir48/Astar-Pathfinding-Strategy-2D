using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace StrategyGame_2DPlatformer
{
    public class HighligtBuildingsAtMousePosition : MonoBehaviour
    {

        public Color unavalaibleColor;
        public Color availaibleColor;
        private Color originalColor; // If a tile is to be de-emphasized, it turns back to its original color.
        private List<Vector3Int> currentTilePositions; // Will depend on mouse position and building size when the class will be over
        private int sizeX;
        private int sizeY;
        Vector3Int previousCellPosToCompare;


        private void Start()
        {
            //sizeX = GetComponent<IPlaceable>().SizeX;
            //sizeY = GetComponent<IPlaceable>().SizeY; //used after attached to building prefab
            sizeX = 2;
            sizeY = 3;
            originalColor = GameData.instance.Tilemap.GetColor(new Vector3Int(1, 1, 1));
            currentTilePositions = new List<Vector3Int>();
        }


        void Update()
        {
            // Get the current cell position under the mouse 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = GameData.instance.Tilemap.WorldToCell(mousePosition);

            if (tilePosition != previousCellPosToCompare) // Instead of comparing the list, I now compare single cell to understand if the mouse position is changed.
            {
                int startX = tilePosition.x - (sizeX - 1) / 2;
                int endX = startX + sizeX - 1;
                int startY = tilePosition.y - (sizeY - 1) / 2;
                int endY = startY + sizeY - 1;
                // Reset the colors of the previously highlighted cells
                foreach (var pos in currentTilePositions)
                {
                    if (!(pos.x >= startX && pos.x <= endX && pos.y >= startY && pos.y <= endY))
                    {
                        ChangeTileColor(pos, Color.white);
                    }
                }

                // Get the positions of the cells around the current cell position
                currentTilePositions.Clear();
                for (int x = startX; x < startX + sizeX; x++)
                {
                    for (int y = startY; y < startY + sizeY; y++)
                    {
                        Vector3Int position = new Vector3Int(x, y, tilePosition.z);
                        currentTilePositions.Add(position);
                    }
                }

                // Highlight the cells that are not occupied
                foreach (var pos in currentTilePositions)
                {
                    Node node = GameData.instance.Graph.GetNodeAtPosition(pos);
                    if (node.isOccupied)
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


        private void ChangeTileColor(Vector3Int pos, Color color)
        {
            GameData.instance.Tilemap.SetTileFlags(pos, TileFlags.None);
            GameData.instance.Tilemap.SetColor(pos, color);
        }

    }
}
