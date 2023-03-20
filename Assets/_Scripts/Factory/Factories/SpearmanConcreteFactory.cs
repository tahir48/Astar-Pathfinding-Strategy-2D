using StrategyGame_2DPlatformer.Contracts;
using UnityEngine;

namespace StrategyGame_2DPlatformer
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
