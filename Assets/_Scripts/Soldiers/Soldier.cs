using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;
using System;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public abstract class Soldier :  MonoBehaviour, ISelectable, IDamageable
    {
        public virtual int Cost { get;}

        protected Node currentNode;
        public abstract void Move();
        public abstract void Attack();
        protected virtual int PopulationOccupied { get; set; }

        #region ISelectable
        public bool IsSelected { get; set; }
        public abstract void OnSelected();
        public abstract void OnDeselected();
        #endregion

        #region IDamageable
        public virtual int MaxHealth { get;}
        public abstract void Damage(int damage);
        public virtual Vector3Int DamageFrom { get; }
  
        #endregion

    }
}
