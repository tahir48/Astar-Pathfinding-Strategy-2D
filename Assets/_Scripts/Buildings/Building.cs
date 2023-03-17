using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;
using System.Collections.Generic;

namespace StrategyGame_2DPlatformer
{
    public abstract class Building : MonoBehaviour, ISelectable, IPlaceable
    {
        protected int health;
        protected int cost;
        public bool IsSelected { get; set; }
        public bool IsPlaced { get; set; }
        public List<Vector3Int> OccupiedPositions { get; set; }
        public int SizeX { get ;}
        public int SizeY { get ;}

        public abstract void OnDeselected();
        public abstract void OnSelected();

    }
}
