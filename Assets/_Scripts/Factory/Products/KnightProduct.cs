using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.Soldiers;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class KnightProduct : MonoBehaviour, IProduct
    {
        public void Initialize()
        { var sold = GetComponent<MeleeSoldier>();
            sold.SetCurrentNodeOnSpawn();
            GameManagement.GameData.instance.DecreaseMoney(sold.Cost);
        }
    }
}
