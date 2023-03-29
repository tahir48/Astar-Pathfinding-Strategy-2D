using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings.UI
{   
    public class BuildingPlacementController : MonoBehaviour
    {
        /// <summary>
        /// This class is responsible for placing the buildings on the map.
        /// It instantiates a building sprite when button is clicked, checks if the position where the building is being placed is valid, 
        /// and handles the final placement of the building on the nodes.
        /// </summary>
        
        public GameObject spritePrefab;
        private bool isOpen;
        GameObject sprite;
        private PlacementPositionHandler _placeBuilding;
        private IPlaceable _placeable;
        private Camera mainCamera;
        private GameData gameData;
        private void Start()
        {
            isOpen = false;
            mainCamera = Camera.main;
            gameData = GameData.instance;
        }

        private void Update()
        {
            if (!isOpen) return;
 
            if (Input.GetMouseButtonDown(1))
            {
                OnPlacementFailed();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!sprite.TryGetComponent(out _placeable))
                {
                    return;
                }
                var building = sprite.GetComponent<Building>();
                if (gameData.Money < building.Cost)
                {
                    Debug.LogWarning("Not Enough Money To Construct! Build A Mill To Earn Some Money");
                    OnPlacementFailed();
                    return;
                }

                if (_placeBuilding.PositionsToPlace != null && _placeable.IsPlaceable)
                {
                    //Set the nodes occupied
                    foreach (var pos in _placeBuilding.PositionsToPlace)
                    {
                        Node node = gameData.Graph.GetNodeAtPosition(pos);
                        node?.SetOccupied(true);
                    }

                    _placeable.OccupiedPositions = _placeBuilding.PositionsToPlace;
                    _placeable.IsPlaced = true;
                    _placeable.OnBuildingPlaced();

                    if (sprite.TryGetComponent(out SpriteFollowMouse spriteFollowMouse)) spriteFollowMouse.enabled = false;
                    if (sprite.TryGetComponent(out HighligtBuildingsAtMousePosition highligtBuildingsAtMousePosition)) highligtBuildingsAtMousePosition.enabled = false;
                    _placeBuilding.enabled = false;

                    Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int tileToPlace = gameData.Tilemap.WorldToCell(mousePos);
                    Vector3 destinationToPlace = gameData.Tilemap.GetCellCenterWorld(tileToPlace);
                    sprite.transform.position = new Vector3(destinationToPlace.x + 0.5f, destinationToPlace.y + 0.5f, 0f);

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
            return;
        }

        public void OnBarracksButtonClick()
        {
            sprite = Instantiate(spritePrefab, Vector3.zero, Quaternion.identity);
            _placeBuilding = sprite.GetComponent<PlacementPositionHandler>();
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            sprite.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            isOpen = true;
        }


    }
}
