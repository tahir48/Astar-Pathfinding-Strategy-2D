using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;
using System.Collections.Generic;

namespace StrategyGame_2DPlatformer
{
    public abstract class Building : MonoBehaviour, ISelectable, IPlaceable
    {
        protected int health;
        protected int cost;
        

        #region IPlaceable
        public bool IsPlaced { get; set; }
        public List<Vector3Int> OccupiedPositions { get; set; }
        public virtual int SizeX { get; set; }
        public virtual int SizeY { get; set; }
        #endregion

        public bool IsSelected { get; set; }

        public abstract void OnDeselected();
        public abstract void OnSelected();

    }
}
