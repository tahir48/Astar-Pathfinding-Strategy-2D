using StrategyGame_2DPlatformer.Core;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class SpearmanConcreteFactory : Factory
    {
        [SerializeField] private SpearmanProduct spearmanPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            GameObject instance = Instantiate(spearmanPrefab.gameObject, position, Quaternion.identity);
            SpearmanProduct newProduct = instance.GetComponent<SpearmanProduct>();
            newProduct.Initialize();
            return newProduct;
        }
    }
}
