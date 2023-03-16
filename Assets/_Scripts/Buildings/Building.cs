using UnityEngine;
using StrategyGame_2DPlatformer.Contracts;

namespace StrategyGame_2DPlatformer
{
    public abstract class Building : MonoBehaviour, ISelectable
    {
        protected int health;
        protected int cost;
        protected int sizeX;
        protected int sizeY;
        public bool IsSelected { get; set; }

        public abstract void OnDeselected();
        public abstract void OnSelected();

    }
}
