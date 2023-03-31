using StrategyGame_2DPlatformer.GraphStructure;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Core
{
    public abstract class Soldier :  MonoBehaviour
    {
        public Node currentNode;
        public virtual int Cost { get; private set; }
        public virtual int PopulationOccupied { get; private set; }
        protected abstract void Move();
        protected virtual int AttackDamage { get; set; }
        protected abstract void Attack(IDamageable damageable);
       
    }
}