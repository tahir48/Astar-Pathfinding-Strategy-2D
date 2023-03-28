using UnityEngine;

namespace StrategyGame_2DPlatformer.Contracts
{
    public interface IDamageable
    {
        bool IsAlive { get; }
        int MaxHealth { get; }
        Vector3Int DamageFrom { get; }
        void Damage(int damage);
        void Die();
    }
}


