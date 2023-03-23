using UnityEngine;

namespace StrategyGame_2DPlatformer.SoldierFactory
{
    public abstract class Factory : MonoBehaviour
    {
        public abstract IProduct GetProduct(Vector3 position);
    }
}
