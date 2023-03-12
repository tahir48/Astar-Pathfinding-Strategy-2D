using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer
{
    public class HighligtMousePosition : MonoBehaviour
    {
        private Color highlightColor;
        private TileBase highlightedTile;
        private Vector3Int previousPosition;
        private Color previousColor;

        private void Start()
        {
            highlightColor = Color.red;
            previousColor = Color.green;
        }

        void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = GameData.instance.Tilemap.WorldToCell(mousePosition);

            if (previousPosition != tilePosition)
            {
                GameData.instance.Tilemap.SetTileFlags(previousPosition, TileFlags.None);
                GameData.instance.Tilemap.SetColor(previousPosition, previousColor);
            }

            highlightedTile = GameData.instance.Tilemap.GetTile(tilePosition);
            if (highlightedTile != null)
            {
                previousPosition = tilePosition;
                previousColor = GameData.instance.Tilemap.GetColor(tilePosition);
                GameData.instance.Tilemap.SetTileFlags(tilePosition, TileFlags.None);
                GameData.instance.Tilemap.SetColor(tilePosition, highlightColor);
            }
        }
    }

}
