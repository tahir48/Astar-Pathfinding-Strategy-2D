using StrategyGame_2DPlatformer.Buildings;
using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StrategyGame_2DPlatformer
{
    public class SelectableBuilding : MonoBehaviour, ISelectable
    {
        private bool _isSelected;
        private SpriteRenderer _spriteRenderer;
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }
        private bool ClickIsNotOnUIandClickIsNotOnBuilding => !EventSystem.current.IsPointerOverGameObject() && !IsClickOnTheBuilding();
        public RectTransform soldierHolder;
        Building building;

        private void Start()
        {
            _isSelected = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            building = GetComponent<Building>();
            if (building?.Name == "Barracks") soldierHolder = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<RectTransform>();
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
            OnBarracksClicked();
        }
        public void OnBarracksClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.MilitaryBuildingSprite;
            GameData.instance.buildingText.text = building.Name;
            if (building?.Name == "Barracks") soldierHolder?.gameObject.SetActive(true);
        }

        public void OnMillClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.productionBuildingSprite;
            GameData.instance.buildingText.text = building.Name;
        }

        public void OnHouseClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.populationBuildingSprite;
            GameData.instance.buildingText.text = building.Name;
        }


        public void OnDeselected()
        {
            if (ClickIsNotOnUIandClickIsNotOnBuilding)
            {
                IsSelected = false;
                _spriteRenderer.color = Color.white;
                if (building?.Name == "Barracks") soldierHolder?.gameObject.SetActive(false);
                GameData.instance.HideInformationMenu();
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
