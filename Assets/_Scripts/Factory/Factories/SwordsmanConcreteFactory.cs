using StrategyGame_2DPlatformer.Contracts;
using UnityEngine;

namespace StrategyGame_2DPlatformer
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
