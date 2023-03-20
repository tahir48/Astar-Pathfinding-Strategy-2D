using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public abstract class Soldier :  MonoBehaviour, ISelectable, IDamageable
    {
        protected Node currentNode;
        public abstract void Move();
        protected virtual int PopulationOccupied { get; set; }

        #region ISelectable
        public bool IsSelected { get; set; }
        public abstract void OnSelected();
        public abstract void OnDeselected();
        #endregion

        #region IDamageable
        public virtual int MaxHealth { get;}
        public abstract void Damage(int damage);
  
        #endregion

    }
}
