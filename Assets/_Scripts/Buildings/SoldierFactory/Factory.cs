using StrategyGame_2DPlatformer.Core;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public abstract class Factory : MonoBehaviour
    {
        public abstract IProduct GetProduct(Vector3 position);
    }
}
