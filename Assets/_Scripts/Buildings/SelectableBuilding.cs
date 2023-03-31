using StrategyGame_2DPlatformer.Core;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class SelectableBuilding : MonoBehaviour, ISelectable
    {
        private bool _isSelected;
        private SpriteRenderer _spriteRenderer;
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }
        private bool ClickIsNotOnUIandClickIsNotOnBuilding => !EventSystem.current.IsPointerOverGameObject() && !IsClickOnTheBuilding();
        Building building;
        private GameData _gamedata;

        private void Start()
        {
            _gamedata = GameData.instance;
            _isSelected = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            building = GetComponent<Building>();
        }

        private void OnMouseDown()
        {
            if (GetComponent<Building>().IsPlaced)
            {
                OnSelected();
            }
        }

        public void OnSelected()
        {
            _isSelected = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            OnBuildingClicked(building);
        }

        public void OnBuildingClicked(Building building)
        {
            _gamedata.ShowInformationMenu();
            _gamedata.buildingText.text = building.Name;
            switch (building.Name)
            {
                case "House":
                    _gamedata.buildingsImageUI.sprite = _gamedata.populationBuildingSprite;
                    break;
                case "Mill":
                    _gamedata.buildingsImageUI.sprite = _gamedata.productionBuildingSprite;
                    break;
                case "Barracks":
                    _gamedata.buildingsImageUI.sprite = _gamedata.MilitaryBuildingSprite;
                    _gamedata.soldierHolder?.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }

        }


        public void OnDeselected()
        {
            if (ClickIsNotOnUIandClickIsNotOnBuilding)
            {
                IsSelected = false;
                _spriteRenderer.color = Color.white;
                if (building.Name == "Barracks") _gamedata.soldierHolder.gameObject.SetActive(false);
                _gamedata.HideInformationMenu();
            }
        }

        private bool IsClickOnTheBuilding()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return GetComponent<SpriteRenderer>().bounds.Contains(mousePosition);
        }

        void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }
        }
    }
}
