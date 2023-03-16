using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.Soldiers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class MilitaryBuilding : Building
    {
        [SerializeField] GameObject swordsmanPrefab;

        #region Production Related Variables
        private Vector3Int _spawnpoint;
        #endregion

        #region Placement Related Variables
        public bool isPlaced;
        public List<Vector3Int> occupiedPositions;
        #endregion
        #region Selection Related Variables
        private Image buildingsImageUI;
        private Image soldierImageUI;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite nullImage;
        [SerializeField] private Sprite swordsmanSprite;
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
            Vector3Int corner = occupiedPositions[0];
            foreach (Vector3Int pos in occupiedPositions)
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

        private void HandleButtonClick()
        {
            FindSpawnPoint();
            Vector3 spawnPoint = GameData.instance.Tilemap.GetCellCenterWorld(_spawnpoint);
            var swordsman = Instantiate(GameData.instance.swordsmanPrefab, spawnPoint, Quaternion.identity);
            swordsman.GetComponent<MeleeSoldier>().SetCurrentNodeOnSpawn();
        }
        #endregion
        private void Start()
        {
            #region Selection Related Variables
            base.IsSelected = false;
            buildingsImageUI = GameObject.FindGameObjectWithTag("BuildingImage").GetComponent<Image>();
            soldierImageUI = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<Image>();
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
            soldierImageUI.sprite = swordsmanSprite;
        }

        void OnMouseDown()
        {
            if (isPlaced)
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
                buildingsImageUI.sprite = nullImage;
                soldierImageUI.sprite = nullImage;
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
