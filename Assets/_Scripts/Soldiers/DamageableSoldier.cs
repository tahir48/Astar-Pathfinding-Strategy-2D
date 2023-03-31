using StrategyGame_2DPlatformer.Core;
using StrategyGame_2DPlatformer.GameManagement;
using System;
using UnityEngine;


namespace StrategyGame_2DPlatformer.Soldiers
{
    public class DamageableSoldier : MonoBehaviour, IDamageable
    {
        public static event Action HealthChanged;

        //Model for MVP Pattern in Health Bar
        private int _currentHealth;
        private bool _isAlive;
        [SerializeField] private int _maxHealth;
        public int CurrentHealth { get { return _currentHealth; } }
        public int MaxHealth { get { return _maxHealth; } }

        public Vector3Int DamageFrom
        {
            get => GetClosestNodeToAttack(GameData.instance.soldier);
        }
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
            soldier.CancelInvoke();
            soldier.currentNode.SetOccupied(false);
            _isAlive = false;
            gameObject.SetActive(false);
        }
        private void UpdateHealth()
        {
            HealthChanged?.Invoke();
        }

        public Vector3Int GetClosestNodeToAttack(Soldier selectedSoldier)
        {
            var damageableSoldier = GetComponent<Soldier>();
            Vector3Int soldierPosition = new Vector3Int(selectedSoldier.currentNode.x, selectedSoldier.currentNode.y, 0);
            Vector3Int closestPosition = new Vector3Int(damageableSoldier.currentNode.x, damageableSoldier.currentNode.y, 0);
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
