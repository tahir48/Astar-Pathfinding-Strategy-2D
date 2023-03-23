using StrategyGame_2DPlatformer.Soldiers;
using UnityEngine;

namespace StrategyGame_2DPlatformer.SoldierFactory.Products
{
    public class SwordsmanProduct : MonoBehaviour, IProduct
    {
        public void Initialize()
        {
            var sold = GetComponent<MeleeSoldier>();
            sold.SetCurrentNodeOnSpawn();
            GameManagement.GameData.instance.SpendMoney(sold.Cost);
        }
    }
}
