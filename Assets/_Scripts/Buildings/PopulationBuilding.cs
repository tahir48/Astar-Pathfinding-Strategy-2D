using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class PopulationBuilding : Building
    {

        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        public override string Name { get { return _name; } }
        public override int Cost { get { return _cost; } }
        #region Damage Related Variables
        private int _currentHealth;
        [SerializeField] private int _maxHealth;
        public override int MaxHealth { get { return _maxHealth; } }
        public override Vector3Int DamageFrom { get { return FindSpawnPoint() + Vector3Int.down; } }
        public Vector3Int takeDamageFrom;
        [SerializeField] private Image _fillBar;
        #endregion
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion
        private SpriteRenderer _spriteRenderer;
        //To make conditional statement self-explanatory
        private bool ClickIsNotOnUIandClickIsNotOnBuilding => !EventSystem.current.IsPointerOverGameObject() && !IsClickOnTheBuilding();

        private void OnEnable()
        {
            _currentHealth = MaxHealth;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDisable()
        {
            GameData.instance.DecreaseCurrentAvailaiblePop(5);
        }

        #region Selection Related Functionality
        void Update()
        {

            if (IsSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }

        }

        public override void OnSelected()
        {
            IsSelected = true;
            _spriteRenderer.color = Color.red;
            OnHouseClicked();
        }

        public void OnHouseClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.populationBuildingSprite;
            GameData.instance.buildingText.text = Name;
        }
        void OnMouseDown()
        {
            if (IsPlaced)
            {
                OnSelected();
            }
        }

        public override void OnDeselected()
        {
            if (ClickIsNotOnUIandClickIsNotOnBuilding)
            {
                // Deselect the building
                IsSelected = false;
                _spriteRenderer.color = Color.white;
                GameData.instance.HideInformationMenu();
            }
        }

        bool IsClickOnTheBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return _spriteRenderer.bounds.Contains(mousePosition);
        }
        #endregion

        #region Damage Related Functionality
        public override void Damage(int damage)
        {
            if (_currentHealth <= damage)
            {
                Destroy(gameObject);
                foreach (var pos in OccupiedPositions)
                {
                    GameData.instance.Graph.GetNodeAtPosition(pos).isOccupied = false;
                }
                return;
            }
            _currentHealth -= damage;
            _fillBar.fillAmount = ((float)_currentHealth / (float)_maxHealth);
        }


        public Vector3Int FindSpawnPoint()
        {
            Vector3Int pos = FindCorner();
            var isRightSideOpen = pos != null && !GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos + Vector3Int.right).isOccupied;
            if (isRightSideOpen)
            {
                takeDamageFrom = pos + Vector3Int.right;
            }
            return takeDamageFrom;
        }
        private Vector3Int FindCorner()
        {
            Vector3Int corner = OccupiedPositions[0];
            foreach (Vector3Int pos in OccupiedPositions)
            {
                if (pos.x > corner.x) corner.x = pos.x;
                if (pos.y > corner.y) corner.y = pos.y;
            }
            return corner;
        }
        #endregion

        #region Placement Related Functionality
        public override void OnBuildingPlaced()
        {
            GameData.instance.IncreaseCurrentAvailaiblePop(5);
            GameData.instance.SpendMoney(5);
        }
        #endregion
    }
}
