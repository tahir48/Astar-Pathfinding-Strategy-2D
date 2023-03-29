using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using System;
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
        private SoldierMovementHandler _soldierMovementHandler;
        IDamageable _targetDamageable;
        private bool isAttacking;
        private Camera _mainCamera;

        private void OnEnable()
        {
            _soldierMovementHandler = GetComponent<SoldierMovementHandler>();
            _soldierMovementHandler.OnMovementComplete += WhenReachedTargetToAttack;
            OnMovementComplete += WhenReachedTargetToAttack;
            GameData.instance.IncreaseCurrentHumanPop(PopulationOccupied);
            selectable = GetComponent<Selectable>();
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

            if (_soldierMovementHandler.IsMoving)
            {
                Move();
            }
        }
        private void HandleRightClick()
        {
            if (Input.GetMouseButtonDown(1) && selectable.IsSelected && !_soldierMovementHandler.IsMoving)
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
                    _soldierMovementHandler.MoveToTarget(currentNode, nextNode);
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
            Node targetNode = GameData.instance.Graph.GetNodeAtPosition(nextNode);
            if (currentNode == targetNode)
            {
                OnMovementComplete?.Invoke();
            }
            else
            {
                _soldierMovementHandler.MoveToTarget(currentNode, targetNode);
            }
        }

        public override void Move()
        {
            _soldierMovementHandler.Move(this, _targetDamageable);
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



