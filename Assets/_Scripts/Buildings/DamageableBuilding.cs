using StrategyGame_2DPlatformer.Buildings;
using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.Soldiers;
using System;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class DamageableBuilding : MonoBehaviour, IDamageable
    {
        public static event Action HealthChanged;

        //Model for MVP Pattern in Health Bar
        private int _currentHealth;
        public int CurrentHealth { get { return _currentHealth; } }

        [SerializeField] private int _maxHealth;
        public int MaxHealth { get { return _maxHealth; } }

        private bool _isAlive;
        public bool IsAlive => _isAlive;
        public Vector3Int DamageFrom { get =>
                GetComponent<Building>().SpawnPoint;
        }

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
            Debug.Log("Current Health  " + _currentHealth);
            UpdateHealth();
        }

        public void Die()
        {
            var positions = GetComponent<Building>().OccupiedPositions;
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

    }

}
