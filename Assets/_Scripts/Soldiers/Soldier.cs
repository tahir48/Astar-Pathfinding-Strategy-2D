using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public abstract class Soldier :  MonoBehaviour, ISelectable
    {
        public bool IsSelected { get; set; }
        protected Node currentNode;
        protected virtual int PopulationOccupied { get; set; }
        public abstract void Move();

        public abstract void OnSelected();
        public abstract void OnDeselected();

    }
}
