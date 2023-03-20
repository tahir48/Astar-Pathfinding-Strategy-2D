using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;
using System.Collections.Generic;

namespace StrategyGame_2DPlatformer
{
    public abstract class Building : MonoBehaviour, ISelectable, IPlaceable, IDamageable
    {
        protected int cost;

        #region IPlaceable
        public bool IsPlaceable { get; set; }
        public bool IsPlaced { get; set; }
        public List<Vector3Int> OccupiedPositions { get; set; }
        public virtual int SizeX { get; set; }
        public virtual int SizeY { get; set; }
        #endregion

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
