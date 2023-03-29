using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.Soldiers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class SoldierMovementHandler : MonoBehaviour
    {
        public event Action OnMovementComplete;

        private List<Node> _pathToWalk;
        private float moveSpeed = 2f;
        private int _indexToVisit;
        private bool isMoving;
        public bool IsMoving => isMoving;
        private bool destinationReached => _indexToVisit == _pathToWalk.Count;
        private void OnEnable()
        {
            _indexToVisit = 0;
            isMoving = false;

        }
        public void MoveToTarget(Node currentNode, Node targetNode)
        {
            if (isMoving) return;
            if (targetNode.isOccupied) return;
            _pathToWalk = AStar.FindPath(currentNode, targetNode);
            if (_pathToWalk == null || _pathToWalk.Count == 0) return;
            currentNode.isOccupied = false;
            isMoving = true;
        }

        public void Move(Soldier soldier, IDamageable targetDamageable)
        {
            Vector3Int destination = new Vector3Int(_pathToWalk[_indexToVisit].x, _pathToWalk[_indexToVisit].y, 0);
            Vector3 targetPosition = GameManagement.GameData.instance.Tilemap.GetCellCenterWorld(destination);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Node currentNode = _pathToWalk[_indexToVisit];
            if (transform.position == targetPosition)
            {
                currentNode = _pathToWalk[_indexToVisit];
                _indexToVisit++;

                if (destinationReached)
                {
                    currentNode.isOccupied = true;
                    soldier.currentNode = currentNode;
                    isMoving = false;
                    if (targetDamageable != null)
                    {
                        OnMovementComplete?.Invoke();
                    }
                    _pathToWalk = null;
                    _indexToVisit = 0;
                }
            }
        }
    }
}
