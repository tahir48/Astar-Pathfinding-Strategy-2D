using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public class MeleeSoldier : Soldier
    {
        #region Damage Related Variables
        private int _currentHealth;
        [SerializeField] private int _maxHealth;
        public override int MaxHealth { get { return _maxHealth; } }
        [SerializeField] private Image _fillBar;
        public override Vector3Int DamageFrom { get => new Vector3Int(currentNode.x, currentNode.y, 0) + Vector3Int.left; }
        public event Action<int> OnMovementComplete;
        #endregion

        private void OnEnable()
        {
            OnMovementComplete += WhenReachedTargetToAttack;
        }

        private void WhenReachedTargetToAttack(int damage)
        {
            hitObj1.Damage(damage);
        }

        [SerializeField] private int meleePopulationOccupied;
        protected override int PopulationOccupied => meleePopulationOccupied;
        private SpriteRenderer _spriteRenderer;
        private List<Node> _pathToWalk;
        private float moveSpeed = 2f;
        private int _indexToVisit;
        private bool isMoving;

        private void Start()
        {
            _currentHealth = MaxHealth;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GameData.instance.CurrentPopulation += PopulationOccupied;
            base.IsSelected = false;
        }
        Node nextNodee;
        IDamageable hitObj1;
        Collider2D collider1;
        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && IsSelected && !isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit && hit.collider.GetComponent<IDamageable>() != null)
                {
                    collider1 = hit.collider;
                    Debug.Log("Attack The Damageable!");
                    IDamageable hitObj = hit.collider.GetComponent<IDamageable>();
                    hitObj1 = hitObj;
                    Vector3Int nextNode = hitObj.DamageFrom;
                    nextNodee = GameData.instance.Graph.GetNodeAtPosition(nextNode);
                    MoveTo(nextNodee);
                    //hitObj.Damage(10);
                }
                else
                {
                    Node nextNode = GameData.instance.Graph.GetNodeAtMouseClick();
                    MoveTo(nextNode);
                }
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
                    if (hitObj1 != null)
                    {
                        OnMovementComplete?.Invoke(20);
                    }
                    hitObj1 = null;

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
        #region Selection Related Functionality
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
        #endregion

        #region Damage related functionality
        public override void Damage(int damage)
        {
            _currentHealth -= damage;
            _fillBar.fillAmount = ((float)_currentHealth / (float)_maxHealth);
        }

        public override void Attack()
        {

        }
        #endregion

    }
}
