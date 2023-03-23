using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer
{
    public class TilemapAvailaibilityCheckerForSingleTile : MonoBehaviour
    {
        //This class is raycast version of "HighligtMousePosition" class. Same responsibility with different functionality.

        [SerializeField] LayerMask tilemapLayer;
        Vector3Int previouslyVisitedTilePos = new Vector3Int();
        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, tilemapLayer);
            Vector3 worldPos = hit.point;
            Vector3Int cellPos = GameData.instance.Tilemap.WorldToCell(worldPos);
            Node node = GameData.instance.Graph.GetNodeAtPosition(cellPos);
            if (cellPos != previouslyVisitedTilePos)
            {
                if (node.isOccupied)
                {
                    ChangeTileColor(cellPos, Color.red);
                    ChangeTileColor(previouslyVisitedTilePos, Color.white);
                }
                else
                {
                    ChangeTileColor(cellPos, Color.green);
                    ChangeTileColor(previouslyVisitedTilePos, Color.white);

                }
                previouslyVisitedTilePos = cellPos;
            }
        }
        private void ChangeTileColor(Vector3Int pos, Color color)
        {
            GameData.instance.Tilemap.SetTileFlags(pos, TileFlags.None);
            GameData.instance.Tilemap.SetColor(pos, color);
        }
    }
}
