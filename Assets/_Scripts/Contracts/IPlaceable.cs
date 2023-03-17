using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Contracts
{
    public interface IPlaceable
    {
        int SizeX { get; set;}
        int SizeY { get; set;}
        bool IsPlaced { get; set; }
        List<Vector3Int> OccupiedPositions { get; set; }
    }
}
