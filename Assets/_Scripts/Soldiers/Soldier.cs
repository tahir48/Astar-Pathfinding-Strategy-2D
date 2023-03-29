using StrategyGame_2DPlatformer.Contracts;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public abstract class Soldier :  MonoBehaviour
    {
        public virtual int Cost { get; private set; }
        public virtual int PopulationOccupied { get; private set; }
        public Node currentNode;
        public abstract void Move();
        public virtual int AttackDamage { get; set; }
        public abstract void Attack(IDamageable damageable);
       
    }
}