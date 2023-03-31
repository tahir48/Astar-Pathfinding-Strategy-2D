using StrategyGame_2DPlatformer.Core;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class SwordsmanConcreteFactory : Factory
    {
        [SerializeField] private SwordsmanProduct swordsmanPrefab;
        public override IProduct GetProduct(Vector3 position)
        {
            GameObject instance = Instantiate(swordsmanPrefab.gameObject, position, Quaternion.identity);
            SwordsmanProduct newProduct = instance.GetComponent<SwordsmanProduct>();
            newProduct.Initialize();
            return newProduct;
        }
    }
}
