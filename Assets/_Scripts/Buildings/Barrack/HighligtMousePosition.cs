using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer
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

            if (GameData.instance.Graph.GetNodeAtMouseClick(GameData.instance.Tilemap,Camera.main,GameData.instance.Graph.Nodes).isOccupied)
            {
                highlightColor = unavalaibleColor;
            }else
            {
                highlightColor = availaibleColor;
            }

            if (previousPosition != tilePosition)
            {
                GameData.instance.Tilemap.SetTileFlags(previousPosition, TileFlags.None);
                GameData.instance.Tilemap.SetColor(previousPosition, previousColor);
            }

            highlightedTile = GameData.instance.Tilemap.GetTile(tilePosition);
            if (highlightedTile != null)
            {
                previousPosition = tilePosition;
                GameData.instance.Tilemap.SetTileFlags(tilePosition, TileFlags.None);
                GameData.instance.Tilemap.SetColor(tilePosition, highlightColor);
            }
        }
    }





}
