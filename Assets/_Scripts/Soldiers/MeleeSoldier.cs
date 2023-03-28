using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public class MeleeSoldier : Soldier
    {
        [SerializeField] private int _cost;
        public override int Cost { get { return _cost; } }
        protected Selectable selectable;
        public event Action OnMovementComplete;

        [SerializeField] private int meleePopulationOccupied;
        [SerializeField] private int _attackDamage;

        public override int AttackDamage
        {
            get { return _attackDamage; }
            set { _attackDamage = value; }
        }

        public override int PopulationOccupied => meleePopulationOccupied;
        private List<Node> _pathToWalk;
        private float moveSpeed = 2f;
        private int _indexToVisit;
        private bool isMoving;
        //Node nextNodee;
        IDamageable _targetDamageable;
        private bool isAttacking;
        private Camera _mainCamera;

        private bool destinationReached => _indexToVisit == _pathToWalk.Count;

        private void OnEnable()
        {
            OnMovementComplete += WhenReachedTargetToAttack;
            GameData.instance.IncreaseCurrentHumanPop(PopulationOccupied);
            selectable = GetComponent<Selectable>();
            _indexToVisit = 0;
            isAttacking = false;
            _mainCamera = Camera.main;
        }

        private void OnDisable()
        {
            GameData.instance.DecreaseCurrentHumanPop(PopulationOccupied);
        }
        private float _attackInterval = 1f;
        private void WhenReachedTargetToAttack()
        {
            if (_targetDamageable == null) return;
            InvokeRepeating("DealDamage", 0f, _attackInterval);
            isAttacking = true;
        }


        private void DealDamage()
        {
            if (_targetDamageable.IsAlive && isAttacking)
            {
                Debug.Log("Damage was given!");
                _targetDamageable.Damage(_attackDamage);
            }
            else
            {
                StopAttack();
            }
        }

        private void StopAttack()
        {
            isAttacking = false;
            CancelInvoke("DealDamage");
            _targetDamageable = null;
            Debug.Log("Stop Attacking the Damageable!");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && isAttacking)
            {
                StopAttack();
            }
            HandleRightClick();
            HandleMovement();
        }
        private void HandleRightClick()
        {
            if (Input.GetMouseButtonDown(1) && selectable.IsSelected && !isMoving)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit && hit.collider.GetComponent<IDamageable>() != null)
                {
                    HandleAttack(hit);
                }
                else
                {
                    Node nextNode = GameData.instance.Graph.GetNodeAtMouseClick();
                    MoveToTarget(nextNode);
                }
            }
        }

        private void HandleAttack(RaycastHit2D hit)
        {
            IDamageable targetDamageable = hit.collider.GetComponent<IDamageable>();
            if (targetDamageable != null)
            {
                Attack(targetDamageable);
            }
        }

        public override void Attack(IDamageable hitObj)
        {
            _targetDamageable = hitObj;
            Vector3Int nextNode = hitObj.DamageFrom;
            Node nextNodee = GameData.instance.Graph.GetNodeAtPosition(nextNode);
            if (currentNode == nextNodee)
            {
                OnMovementComplete?.Invoke();
            }
            else
            {
                MoveToTarget(nextNodee);
            }
        }


        public void MoveToTarget(Node targetNode)
        {
            if (isMoving) return;
            if (targetNode.isOccupied) return;
            _pathToWalk = AStar.FindPath(currentNode, targetNode);
            if (_pathToWalk == null || _pathToWalk.Count == 0) return;
            currentNode.isOccupied = false;
            isMoving = true;
        }

        private void HandleMovement()
        {
            if (isMoving)
            {
                Move();
            }
        }

        public override void Move()
        {
            Vector3Int destination = new Vector3Int(_pathToWalk[_indexToVisit].x, _pathToWalk[_indexToVisit].y, 0);
            Vector3 targetPosition = GameData.instance.Tilemap.GetCellCenterWorld(destination);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                currentNode = _pathToWalk[_indexToVisit];
                _indexToVisit++;

                if (destinationReached)
                {
                    currentNode.isOccupied = true;
                    isMoving = false;
                    if (_targetDamageable != null)
                    {
                        OnMovementComplete?.Invoke();
                    }
                    _pathToWalk = null;
                    _indexToVisit = 0;
                }
            }
        }

        public void SetCurrentNodeOnSpawn()
        {
            var currentWorldpos = transform.position;
            Vector3Int currentTilePosition = GameData.instance.Tilemap.WorldToCell(currentWorldpos);
            Node nodeToAssign = GameData.instance.Graph.GetNodeAtPosition(currentTilePosition);
            currentNode = nodeToAssign;
        }

    }
}



