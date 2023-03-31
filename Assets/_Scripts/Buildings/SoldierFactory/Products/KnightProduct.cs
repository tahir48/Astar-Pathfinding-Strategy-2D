using StrategyGame_2DPlatformer.Core;
using StrategyGame_2DPlatformer.Soldiers;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class KnightProduct : MonoBehaviour, IProduct
    {
        public void Initialize()
        { var sold = GetComponent<MeleeSoldier>();
            sold.SetCurrentNodeOnSpawn();
            GameManagement.GameData.instance.SpendMoney(sold.Cost);
        }
    }
}
