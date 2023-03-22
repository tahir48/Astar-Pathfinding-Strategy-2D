using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class BuildingPresenter : MonoBehaviour
    {
        public GameObject spritePrefab;
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
                OnPlacementFailed();
            }

            if (isOpen && Input.GetMouseButtonDown(0))
            {
                if (GameData.instance.Money < sprite.GetComponent<Building>().Cost)
                {
                    Debug.LogWarning("Not Enough Money To Construct! Build A Mill To Earn Some Money");
                    OnPlacementFailed();
                    return;
                }
                if (_placeBuilding.PositionsToPlace != null && sprite.GetComponent<IPlaceable>().IsPlaceable)
                {
                    Debug.Log("_placeBuilding.PositionsToPlace  " + _placeBuilding.PositionsToPlace.Count);
                    foreach (var pos in _placeBuilding.PositionsToPlace)
                    {
                        Node node = GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos);
                        node.isOccupied = true;
                    }

                    sprite.GetComponent<IPlaceable>().OccupiedPositions = _placeBuilding.PositionsToPlace;
                    sprite.GetComponent<IPlaceable>().IsPlaced = true;
                    sprite.GetComponent<IPlaceable>().OnPlaced();
                    sprite.GetComponent<SpriteFollowMouse>().enabled = false;
                    _placeBuilding.enabled = false;
                    sprite.GetComponent<HighligtBuildingsAtMousePosition>().enabled = false;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int tileToPlace = GameData.instance.Tilemap.WorldToCell(mousePos);
                    Vector3 destinationToPlace = GameData.instance.Tilemap.GetCellCenterWorld(tileToPlace);
                    sprite.transform.position = new Vector3(destinationToPlace.x, destinationToPlace.y, 0f);
                    isOpen = false;
                }
                else
                {
                    Debug.LogWarning("The position is not availaible to place the building.");
                    OnPlacementFailed();
                }

            }


        }

        private void OnPlacementFailed()
        {
            Destroy(sprite);
            isOpen = false;
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
