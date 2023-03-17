using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Contracts
{
    public interface IPlaceable
    {
        public int SizeX { get;}
        public int SizeY { get;}
        public bool IsPlaced { get; set; }
        public List<Vector3Int> OccupiedPositions { get; set; }
    }
}
