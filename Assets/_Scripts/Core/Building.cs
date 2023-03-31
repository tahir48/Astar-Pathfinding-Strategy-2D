using UnityEngine;
using System.Collections.Generic;

namespace StrategyGame_2DPlatformer.Core
{
    public abstract class Building : MonoBehaviour, IPlaceable
    {
        public virtual int Cost { get; protected set; }
        public virtual string Name { get; protected set; }

        #region IPlaceable
        bool IPlaceable.IsPlaceable { get => IsPlaceable; set => IsPlaceable = value; }
        public bool IsPlaceable { get; protected set; }

        bool IPlaceable.IsPlaced { get => IsPlaced; set => IsPlaced = value; }
        public bool IsPlaced { get; protected set; }

        List<Vector3Int> IPlaceable.OccupiedPositions { get => OccupiedPositions; set => OccupiedPositions = value; }
        public List<Vector3Int> OccupiedPositions { get; protected set; }

        int IPlaceable.SizeX { get => SizeX; set => SizeX = value; }
        public virtual int SizeX { get; protected set; }

        int IPlaceable.SizeY { get => SizeY; set => SizeY = value; }
        public virtual int SizeY { get; protected set; }
        public abstract void OnBuildingPlaced();
        #endregion
    }
}
