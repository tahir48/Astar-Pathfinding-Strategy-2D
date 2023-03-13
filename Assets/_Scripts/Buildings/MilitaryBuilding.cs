using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class MilitaryBuilding : Building
    {
        #region Placement Related Variables
        private Vector3Int _spawnpoint;
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
        bool isSelected = false;
        #endregion
        private void FindSpawnPoint()
        {
            Vector3Int pos = FindCorner();
            if (pos != null && !GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos + Vector3Int.right).isOccupied)
            {
                _spawnpoint = pos + Vector3Int.right;
                GameManagement.GameData.instance.Tilemap.SetTileFlags(_spawnpoint, TileFlags.None);
                GameManagement.GameData.instance.Tilemap.SetColor(_spawnpoint, Color.blue);
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
        private void Start()
        {
            buildingsImageUI = GameObject.FindGameObjectWithTag("BuildingImage").GetComponent<Image>();
            soldierImageUI = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<Image>();
            _spriteRender = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                FindSpawnPoint();
            }

            if (isSelected && Input.GetMouseButtonDown(0))
            {
                // Check if the clicked object is not the building itself
                if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
                {
                    // Deselect the building
                    isSelected = false;
                    _spriteRender.color = Color.white;
                    buildingsImageUI.sprite = nullImage;
                    soldierImageUI.sprite = nullImage;
                }
            }
        }

        bool IsClickOnBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return _spriteRender.bounds.Contains(mousePosition);
        }


        #region Selection Related Functionality
        public void OnBarracksClicked()
        {
            buildingsImageUI.sprite = sprite;
            soldierImageUI.sprite = swordsmanSprite;
        }

        void OnMouseDown()
        {
            if (isPlaced)
            {
                isSelected = true;
                _spriteRender.color = Color.red;
                OnBarracksClicked();
            }
        }
        #endregion

    }
}
