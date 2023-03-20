using UnityEngine;

namespace StrategyGame_2DPlatformer.Contracts
{
    public interface IDamageable
    {
        int MaxHealth { get; }
        Vector3Int DamageFrom { get;}
        void Damage(int damage);
    }
}


