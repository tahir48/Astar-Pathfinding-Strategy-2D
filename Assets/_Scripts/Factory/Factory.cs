using StrategyGame_2DPlatformer.Contracts;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public abstract class Factory : MonoBehaviour
    {
        public abstract IProduct GetProduct(Vector3 position);
    }
}
