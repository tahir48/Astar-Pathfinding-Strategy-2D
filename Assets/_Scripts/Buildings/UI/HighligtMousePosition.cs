using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer.Buildings.UI
{
    public class HighligtMousePosition : MonoBehaviour
    {
        private Color highlightColor;
        public Color unavalaibleColor;
        public Color availaibleColor;
        private TileBase highlightedTile;
        private Vector3Int previousPosition;
        private Color previousColor;
        private bool isOccupied;

        private void Start()
        {
            unavalaibleColor = Color.red;
            availaibleColor = Color.green;
            highlightColor = availaibleColor;
            previousColor = GameData.instance.Tilemap.GetColor(new Vector3Int(1, 1, 1));
        }

        void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = GameData.instance.Tilemap.WorldToCell(mousePosition);

            if (GameData.instance.Graph.GetNodeAtMouseClick().isOccupied)
            {
                highlightColor = unavalaibleColor;
            }else
            {
                highlightColor = availaibleColor;
            }

            if (previousPosition != tilePosition)
            {
                ChangeTileColor(previousPosition, previousColor);
            }

            highlightedTile = GameData.instance.Tilemap.GetTile(tilePosition);
            if (highlightedTile != null)
            {
                previousPosition = tilePosition;
                ChangeTileColor(tilePosition, highlightColor);
            }
        }

        private void ChangeTileColor(Vector3Int pos, Color color)
        {
            GameData.instance.Tilemap.SetTileFlags(pos, TileFlags.None);
            GameData.instance.Tilemap.SetColor(pos, color);
        }
    }
}
