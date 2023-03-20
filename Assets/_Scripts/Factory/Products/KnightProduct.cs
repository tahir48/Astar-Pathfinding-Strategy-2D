using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.Soldiers;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class KnightProduct : MonoBehaviour, IProduct
    {
        public void Initialize()
        {
            GetComponent<MeleeSoldier>().SetCurrentNodeOnSpawn();
        }
    }
}
