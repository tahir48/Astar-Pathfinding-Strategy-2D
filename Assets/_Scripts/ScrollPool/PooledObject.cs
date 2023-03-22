using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class PooledObject : MonoBehaviour
    {
        private ScrollPool pool;
        public ScrollPool Pool { get => pool; set => pool = value; }
    }
}
