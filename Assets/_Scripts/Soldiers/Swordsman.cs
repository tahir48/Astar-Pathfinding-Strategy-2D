using System.Collections.Generic;
using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public class Swordsman : Soldier
    {
        #region Population Related Functionality
        [SerializeField] private int swordsmanPopulationOccupied;
        protected override int PopulationOccupied
        {
            get { return swordsmanPopulationOccupied; }
            set { swordsmanPopulationOccupied = value; }
        }
        #endregion
        #region Selection Related Variables
        private Color _firstColor;
        private SpriteRenderer _spriteRenderer;
        #endregion
        #region Movement Related Variables
        private List<Node> _pathToWalk;
        private float moveSpeed = 2f;
        private int _indexToVisit;
        private bool isMoving;
        #endregion
        private void Start()
        {
            #region Selection Related Assignments
            base.isSelected = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _firstColor = _spriteRenderer.color;
            #endregion
            #region Population Related Variables
            GameData.instance.CurrentPopulation += PopulationOccupied;
            #endregion
        }
        void Update()
        {
            #region Movement Related Functionality
            if (Input.GetMouseButtonDown(1) && base.isSelected && !isMoving)
            {
                _indexToVisit = 0;
                Node nextNode;
                nextNode = GameData.instance.Graph.GetNodeAtMouseClick();
                if (nextNode == null || nextNode.isOccupied) return;
                _pathToWalk = AStar.FindPath(currentNode, nextNode);
                currentNode.isOccupied = false;
                isMoving = true;
                base.isSelected = false;
                _spriteRenderer.color = _firstColor;
            }
            if (isMoving)
            {
                Move();
            }
            #endregion
            #region Selection Related Functionality
            if (isSelected && Input.GetMouseButtonDown(0))
            {
                // Check if the clicked object is not the building itself
                if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
                {
                    // Deselect the building
                    isSelected = false;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            #endregion
        }
        #region Movement Related Functionality
        public void SetCurrentNodeOnSpawn()
        {
            var currentWorldpos = transform.position;
            Vector3Int currentTilePosition = GameData.instance.Tilemap.WorldToCell(currentWorldpos);
            Node nodeToAssign = GameData.instance.Graph.GetNodeAtPosition(currentTilePosition);
            currentNode = nodeToAssign;
        }

        public override void Move()
        {
            Vector3 targetPosition = new Vector3(_pathToWalk[_indexToVisit].x, _pathToWalk[_indexToVisit].y, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                currentNode = _pathToWalk[_indexToVisit];
                _indexToVisit++;
                if (_indexToVisit >= _pathToWalk.Count)
                {
                    currentNode.isOccupied = true;
                    isMoving = false;
                }
            }
        }
        #endregion
        #region Selection Related Functionality
        void OnMouseDown()
        {
            base.isSelected = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        bool IsClickOnBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return GetComponent<SpriteRenderer>().bounds.Contains(mousePosition);
        }

        #endregion

    }
}
