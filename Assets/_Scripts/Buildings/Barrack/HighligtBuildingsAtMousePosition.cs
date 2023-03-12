using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

namespace StrategyGame_2DPlatformer
{
    public class HighligtBuildingsAtMousePosition : MonoBehaviour
    {
        private Color highlightColor;
        public Color unavalaibleColor;
        public Color availaibleColor;
        private TileBase highlightedTile;
        private Vector3Int previousPosition;
        private Color previousColor;
        private bool isOccupied;
        private Vector3Int currentPosition;
        private List<Vector3Int> currentTilePositions;
        private List<Vector3Int> previousPositions;
        private int sizeX;
        private int sizeY;

        private void Start()
        {
            sizeX = 2;
            sizeY = 2;
            unavalaibleColor = Color.red;
            availaibleColor = Color.green;
            highlightColor = availaibleColor;
            previousColor = GameData.instance.Tilemap.GetColor(new Vector3Int(1, 1, 1));
            currentTilePositions = new List<Vector3Int>();
            previousPositions = new List<Vector3Int>();
        }

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

            if (occupiedCount != 0)
            {
                highlightColor = unavalaibleColor;
            }
            else
            {
                highlightColor = availaibleColor;
            }

            // Highlight the current tiles
            ChangeTileColors(currentTilePositions, highlightColor);
            // Unhighlight the previous tiles that are not in the current tiles
            foreach (var pos in previousPositions.Except(currentTilePositions))
            {
                GameData.instance.Tilemap.SetTileFlags(pos, TileFlags.None);
                GameData.instance.Tilemap.SetColor(pos, Color.black);
            }
            // Save the current positions as previous positions for the next frame
            previousPositions = currentTilePositions;
        }

        private void ChangeTileColors(List<Vector3Int> positions, Color color)
        {
            foreach (var pos in positions)
            {
                GameData.instance.Tilemap.SetTileFlags(pos, TileFlags.None);
                GameData.instance.Tilemap.SetColor(pos, color);
            }

        }
    }
}
