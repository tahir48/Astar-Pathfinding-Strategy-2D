using StrategyGame_2DPlatformer.Buildings;
using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.Soldiers;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer
{
    public class DamageableBuilding : MonoBehaviour, IDamageable
    {
        /// <summary>
        /// This class is responsible for keeping data and updating the health of the building i.e. Model.
        /// </summary>
        /// 
        public static event Action HealthChanged;
        private int _currentHealth;
        [SerializeField] 
        private int _maxHealth;
        private bool _isAlive;
        private Building _building;
        public int CurrentHealth { get { return _currentHealth; } }
        public int MaxHealth { get { return _maxHealth; } }
        public bool IsAlive => _isAlive;
        public Vector3Int DamageFrom
        {
            get => GetClosestNodeToAttack(_gameData.soldier);
        }

        private GameData _gameData;
        private void Start()
        {
            _currentHealth = _maxHealth;
            _isAlive = true;
            _building = GetComponent<Building>();
            _gameData = GameData.instance;
        }

        public void Damage(int damage)
        {
            if (_currentHealth <= damage)
            {
                Die();
                return;
            }
            _currentHealth -= damage;
            UpdateHealth();
        }

        public void Die()
        {
            var positions = _building.OccupiedPositions;
            foreach (var pos in positions)
            {
                GameData.instance.Graph.GetNodeAtPosition(pos).isOccupied = false;
            }
            _isAlive = false;
            gameObject.SetActive(false);
        }

        private void UpdateHealth()
        {
            HealthChanged?.Invoke();
        }

        public Vector3Int GetClosestNodeToAttack(Soldier soldier)
        {
            Vector3Int soldierPosition = new Vector3Int(soldier.currentNode.x, soldier.currentNode.y, 0);
            Vector3Int closestPosition = _building.OccupiedPositions[0];
            float closestDistance = Vector3Int.Distance(soldierPosition, closestPosition);

            for (int i = 1; i < _building.OccupiedPositions.Count; i++)
            {
                Vector3Int occupiedPosition = _building.OccupiedPositions[i];
                float distance = Vector3Int.Distance(soldierPosition, occupiedPosition);

                if (distance < closestDistance)
                {
                    closestPosition = occupiedPosition;
                    closestDistance = distance;
                }
            }
            Vector3Int attackPoint;
            if (soldierPosition.x < closestPosition.x)
            {
                attackPoint = new Vector3Int(closestPosition.x - 1, closestPosition.y, 0);
            }
            else if (soldierPosition.x > closestPosition.x)
            {
                attackPoint = new Vector3Int(closestPosition.x + 1, closestPosition.y, 0);
            }
            else if (soldierPosition.y < closestPosition.y)
            {
                attackPoint = new Vector3Int(closestPosition.x, closestPosition.y - 1, 0);
            }
            else
            {
                attackPoint = new Vector3Int(closestPosition.x, closestPosition.y + 1, 0);
            }
            return attackPoint;
        }

    }

}
