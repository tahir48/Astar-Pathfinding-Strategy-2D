using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer
{
    public class BarrackPresenter : MonoBehaviour
    {
        public GameObject spritePrefab;
        public int spriteWidth = 4;
        public int spriteHeight = 4;
        private bool isOpen;
        GameObject sprite;
        private PlaceBuilding _placeBuilding;
        private void Start()
        {
            isOpen = false;
        }

        private void Update()
        {
            if (isOpen && Input.GetMouseButtonDown(1))
            {
                Destroy(sprite);
                isOpen = false;
            }

            if (isOpen && Input.GetMouseButtonDown(0))
            {
                
                if (_placeBuilding.PositionsToPlace != null)
                {
                    Debug.Log("_placeBuilding.PositionsToPlace  " + _placeBuilding.PositionsToPlace.Count);
                    foreach (var pos in _placeBuilding.PositionsToPlace)
                    {
                        Node node = GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos);
                        Debug.Log(node.x);
                        node.isOccupied = true;
                    }
                }
                else
                {
                    Debug.LogWarning("The position is not availaible to place the building.");
                }
                sprite.GetComponent<SpriteFollowMouse>().enabled = false;
                _placeBuilding.enabled = false;
                sprite.GetComponent<MilitaryBuilding>().isPlaced = true;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tileToPlace = GameData.instance.Tilemap.WorldToCell(mousePos);
                Vector3 destinationToPlace = GameData.instance.Tilemap.GetCellCenterWorld(tileToPlace);
                sprite.transform.position = new Vector3(destinationToPlace.x, destinationToPlace.y, 0f);
                isOpen = false;
            }


        }
        public void OnBarracksButtonClick()
        {
            sprite = Instantiate(spritePrefab, Vector3.zero, Quaternion.identity);
            _placeBuilding = sprite.GetComponent<PlaceBuilding>();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sprite.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            isOpen = true;
        }


    }
}
