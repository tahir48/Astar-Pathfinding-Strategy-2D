using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.Soldiers;
using StrategyGame_2DPlatformer.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class MilitaryBuilding : Building
    {
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
        private Image buildingsImageUI;
        private Image soldierImageUI;
        public RectTransform soldierHolder;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite nullImage;
        [SerializeField] private Sprite swordsmanSprite;
        [SerializeField] private Sprite spearmanSprite;
        [SerializeField] private Sprite knightSprite;
        private SpriteRenderer _spriteRender;
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
            #region Selection Related Variables
            base.IsSelected = false;
            buildingsImageUI = GameObject.FindGameObjectWithTag("BuildingImage").GetComponent<Image>();
            //soldierImageUI = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<Image>();
            soldierHolder = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<RectTransform>();
            _spriteRender = GetComponent<SpriteRenderer>();
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
            return _spriteRender.bounds.Contains(mousePosition);
        }
        public void OnBarracksClicked()
        {
            buildingsImageUI.sprite = sprite;
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
                _spriteRender.color = Color.white;
                soldierHolder.gameObject.SetActive(false);
                buildingsImageUI.sprite = nullImage;
            }
        }

        public override void OnSelected()
        {
            IsSelected = true;
            _spriteRender.color = Color.red;
            OnBarracksClicked();
        }
        #endregion

    }
}
