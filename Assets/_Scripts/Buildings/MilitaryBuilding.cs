using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.Soldiers;
using StrategyGame_2DPlatformer.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace StrategyGame_2DPlatformer
{
    public class MilitaryBuilding : Building
    {
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

        #region Production Related Variables
        private Vector3Int _spawnpoint;
        #endregion

        #region Selection Related Variables
        public RectTransform soldierHolder;
        [SerializeField] private Sprite nullImage;
        [SerializeField] private Sprite swordsmanSprite;
        [SerializeField] private Sprite spearmanSprite;
        [SerializeField] private Sprite knightSprite;
        private SpriteRenderer _spriteRenderer;
        #endregion
        #region Production Related Functionality
        private void FindSpawnPoint()
        {
            Vector3Int pos = FindCorner();
            if (pos != null && !GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos + Vector3Int.right).isOccupied)
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
        }

        private void OnDisable()
        {
            SpawnEvent.onSpawnButtonClick -= HandleButtonClick;
        }

        private void HandleButtonClick(string soldierName)
        {
            if (!IsSelected) return;
            //This method will replace with factory very soon
            FindSpawnPoint();
            Vector3 spawnPoint = GameData.instance.Tilemap.GetCellCenterWorld(_spawnpoint);
            var _prefab = GameData.instance.swordsmanPrefab;

            if (spawnPoint != null && soldierName != null)
            {
                if (soldierName == "swordsman")
                {
                    _prefab = GameData.instance.swordsmanPrefab;
                }
                else if (soldierName == "spearman")
                {
                    _prefab = GameData.instance.spearmanPrefab;
                }
                else
                {
                    _prefab = GameData.instance.knightPrefab;
                }
            }
            var swordsman = Instantiate(_prefab, spawnPoint, Quaternion.identity);
            swordsman.GetComponent<MeleeSoldier>().SetCurrentNodeOnSpawn();
        }
        #endregion
        private void Start()
        {
            #region Damage Related Assignments
            _currentHealth = _maxHealth;
            #endregion

            #region Selection Related Variables
            IsSelected = false;
            soldierHolder = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<RectTransform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            #endregion
        }
        void Update()
        {
            #region Selection Related Functionality
            if (IsSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }
            #endregion
        }



        #region Selection Related Functionality
        bool IsClickOnBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return _spriteRenderer.bounds.Contains(mousePosition);
        }
        public void OnBarracksClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.MilitaryBuildingSprite;
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
            if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
            {
                // Deselect the building
                IsSelected = false;
                _spriteRenderer.color = Color.white;
                soldierHolder.gameObject.SetActive(false);
                GameData.instance.buildingsImageUI.sprite = nullImage;
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
        #region Damage related functionality
        public override void Damage(int damage)
        {
            if (_currentHealth <= damage) { Destroy(gameObject); return; }
            _currentHealth -= damage;
            _fillBar.fillAmount = ((float)_currentHealth / (float)_maxHealth);

        }
        #endregion

    }
}
