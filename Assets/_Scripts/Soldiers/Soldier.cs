using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StrategyGame_2DPlatformer.Soldiers
{
    public abstract class Soldier : MonoBehaviour
    {
        protected bool isSelected;
        protected Node currentNode;
        protected virtual int PopulationOccupied { get; set; }
        public abstract void Move();

    }
}
