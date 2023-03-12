using System.Collections.Generic;
using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public class Swordsman : Soldier
    {
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
            populationOccupied = 1;
            currentNode = GameData.instance.Graph.Nodes[0];
            transform.position = new Vector3(GameData.instance.Graph.Nodes[0].x, GameData.instance.Graph.Nodes[0].y, 0);
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
        }

        #region Movement Related Functionality
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
        #endregion

    }
}
