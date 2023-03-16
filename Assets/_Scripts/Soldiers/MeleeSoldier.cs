using StrategyGame_2DPlatformer.GameManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace StrategyGame_2DPlatformer.Soldiers
{
    public class MeleeSoldier : Soldier
    {
        [SerializeField] private int meleePopulationOccupied;
        protected override int PopulationOccupied => meleePopulationOccupied;
        private SpriteRenderer _spriteRenderer;
        private List<Node> _pathToWalk;
        private float moveSpeed = 2f;
        private int _indexToVisit;
        private bool isMoving;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GameData.instance.CurrentPopulation += PopulationOccupied;
            base.IsSelected = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && IsSelected && !isMoving)
            {
                Node nextNode = GameData.instance.Graph.GetNodeAtMouseClick();
                MoveTo(nextNode);
            }

            if (isMoving)
            {
                Move();
            }

            if (IsSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }
        }

        public void MoveTo(Node targetNode)
        {
            if (isMoving) return;
            if (targetNode.isOccupied) return;
            _pathToWalk = AStar.FindPath(currentNode, targetNode);
            if (_pathToWalk == null || _pathToWalk.Count == 0) return;
            currentNode.isOccupied = false;
            isMoving = true;
            _indexToVisit = 0;
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

        private bool IsClickOnBuilding()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return GetComponent<SpriteRenderer>().bounds.Contains(mousePosition);
        }

        public void SetCurrentNodeOnSpawn()
        {
            var currentWorldpos = transform.position;
            Vector3Int currentTilePosition = GameData.instance.Tilemap.WorldToCell(currentWorldpos);
            Node nodeToAssign = GameData.instance.Graph.GetNodeAtPosition(currentTilePosition);
            currentNode = nodeToAssign;
        }

        private void OnMouseDown()
        {
            OnSelected();
        }
        public override void OnSelected()
        {
            IsSelected = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        public override void OnDeselected()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
            {
                IsSelected = false;
                _spriteRenderer.color = Color.white;
            }
        }
    }
}
