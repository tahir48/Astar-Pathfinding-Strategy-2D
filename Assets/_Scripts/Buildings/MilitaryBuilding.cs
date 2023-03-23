using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.UI;
using StrategyGame_2DPlatformer.SoldierFactory;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using StrategyGame_2DPlatformer.SoldierFactory.Factories;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class MilitaryBuilding : Building
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        public override string Name { get { return _name; } }
        public override int Cost { get { return _cost; } }

        #region Production Related Variables
        public enum FactoryStates { Swordsman, Spearman, Knight }
        public FactoryStates factoryState;
        private Factory swordsmanFactory;
        private Factory spearmanFactory;
        private Factory knightFactory;
        private Factory _currentFactory;
        private Vector3Int _spawnpoint;
        #endregion
        #region Selection Related Variables
        public RectTransform soldierHolder;
        private SpriteRenderer _spriteRenderer;
        #endregion
        #region Damage Related Variables
        private int _currentHealth;
        public override Vector3Int DamageFrom { get => _spawnpoint + Vector3Int.down; }
        [SerializeField] private int _maxHealth;
        public override int MaxHealth { get { return _maxHealth; } }
        [SerializeField] private Image _fillBar;
        #endregion
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion

        //To make conditional statement self-explanatory
        private bool ClickIsNotOnUIandClickIsNotOnBuilding => !EventSystem.current.IsPointerOverGameObject() && !IsClickOnTheBuilding();

        private void Start()
        {
            //Damage Related Assignment
            _currentHealth = _maxHealth;
            #region Selection Related Variables
            IsSelected = false;
            soldierHolder = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<RectTransform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            #endregion
        }

        #region Selection Related Functionality
        void Update()
        {

            if (IsSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }
        }

        bool IsClickOnTheBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return _spriteRenderer.bounds.Contains(mousePosition);
        }
        public void OnBarracksClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.MilitaryBuildingSprite;
            GameData.instance.buildingText.text = Name;
            soldierHolder.gameObject.SetActive(true);
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
                soldierHolder.gameObject.SetActive(false);
                GameData.instance.HideInformationMenu();
            }
        }

        public override void OnSelected()
        {
            IsSelected = true;
            _spriteRenderer.color = Color.red;
            OnBarracksClicked();
        }

        #endregion   
        #region Production Related Functionality
        private void FindSpawnPoint()
        {
            Vector3Int pos = FindCorner();
            var isRightSideOpen = pos != null && !GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos + Vector3Int.right).isOccupied;
            if (isRightSideOpen)
            {
                _spawnpoint = pos + Vector3Int.right;
            }
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


        private void OnEnable()
        {
            SpawnEvent.onSpawnButtonClick += HandleButtonClick;
            swordsmanFactory = FindObjectOfType<SwordsmanConcreteFactory>();
            knightFactory = FindObjectOfType<KnightConcreteFactory>();
            spearmanFactory = FindObjectOfType<SpearmanConcreteFactory>();
            ChangeFactoryState(FactoryStates.Swordsman);
        }

        private void OnDisable()
        {
            SpawnEvent.onSpawnButtonClick -= HandleButtonClick;
        }

        private void HandleButtonClick(string soldierName)
        {
            if (!IsSelected) return;
            if (GameData.instance.CurrentPopulationSize - GameData.instance.CurrentHumanPopulationSize <= 0) return;
            FindSpawnPoint();
            Vector3 spawnPoint = GameData.instance.Tilemap.GetCellCenterWorld(_spawnpoint);
            if (Enum.TryParse(soldierName, out factoryState))
            {
                ChangeFactoryState(factoryState);
            }

            if (spawnPoint != null && soldierName != null)
            {
                _currentFactory?.GetProduct(spawnPoint);
            }
        }
        public void ChangeFactoryState(FactoryStates state) //Event caller
        {
            factoryState = state;
            Debug.Log("Factory state has been " + state);
            switch (state)
            {
                case FactoryStates.Swordsman:
                    _currentFactory = swordsmanFactory;
                    break;
                case FactoryStates.Spearman:
                    _currentFactory = spearmanFactory;
                    break;
                case FactoryStates.Knight:
                    _currentFactory = knightFactory;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Placement Related Functionality
        public override void OnBuildingPlaced()
        {
            GameData.instance.SpendMoney(Cost);
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
        #endregion


    }
}
