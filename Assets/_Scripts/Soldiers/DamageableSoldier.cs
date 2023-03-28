using StrategyGame_2DPlatformer.Buildings;
using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.Soldiers;
using System;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class DamageableSoldier : MonoBehaviour, IDamageable
    {
        public static event Action HealthChanged;

        //Model for MVP Pattern in Health Bar
        private int _currentHealth;
        public int CurrentHealth { get { return _currentHealth; } }

        [SerializeField] private int _maxHealth;
        public int MaxHealth { get { return _maxHealth; } }

        //public Vector3Int DamageFrom
        //{
        //    get => new Vector3Int(
        //        GetComponent<Soldier>().currentNode.x,
        //        GetComponent<Soldier>().currentNode.y, 0)
        //        + Vector3Int.right;
        //} 
        public Vector3Int DamageFrom
        {
            get => GetClosestNodeToAttack(GameData.instance.soldier);
        }
        private bool _isAlive;
        public bool IsAlive => _isAlive;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _isAlive = true;
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
            var soldier = GetComponent<Soldier>();
            GameData.instance.DecreaseCurrentHumanPop(soldier.PopulationOccupied);
            soldier.currentNode.isOccupied = false;
            _isAlive = false;
            gameObject.SetActive(false);
        }
        private void UpdateHealth()
        {
            HealthChanged?.Invoke();
        }

        // Buraya en yakýn yeri soldierden soldiere de hesaplaa
        public Vector3Int GetClosestNodeToAttack(Soldier selectedSoldier)
        {
            Vector3Int soldierPosition = new Vector3Int(selectedSoldier.currentNode.x, selectedSoldier.currentNode.y, 0);
            Vector3Int closestPosition = new Vector3Int(GetComponent<Soldier>().currentNode.x, GetComponent<Soldier>().currentNode.y, 0);
            //TileBase closestTile = null;

            Vector3Int attackPoint = Vector3Int.right;
            if (soldierPosition.x < closestPosition.x)
            {
                attackPoint = closestPosition + Vector3Int.left;
            }
            else if (soldierPosition.x > closestPosition.x)
            {
                attackPoint = closestPosition + Vector3Int.right;
            }
            else if (soldierPosition.y < closestPosition.y)
            {
                attackPoint = closestPosition + Vector3Int.down;
            }
            else
            {
                attackPoint = closestPosition + Vector3Int.up;
            }
            return attackPoint;
        }
    }
}
