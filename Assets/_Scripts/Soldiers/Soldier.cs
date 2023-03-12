using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StrategyGame_2DPlatformer.Soldiers
{
    public abstract class Soldier : MonoBehaviour
    {
        protected bool isSelected;
        public int populationOccupied;
        protected Node currentNode;
        public abstract void Move();

    }
}
