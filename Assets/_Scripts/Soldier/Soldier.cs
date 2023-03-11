using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StrategyGame_2DPlatformer.Soldier
{
    public abstract class Soldier : MonoBehaviour
    {
        protected int isSelected;
        public int populationOccupied;
        protected Node currentNode;
        public abstract void Move();

    }
}
