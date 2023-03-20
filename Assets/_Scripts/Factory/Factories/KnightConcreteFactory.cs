using StrategyGame_2DPlatformer.Contracts;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class KnightConcreteFactory : Factory
    {
        [SerializeField] private KnightProduct knightPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            GameObject instance = Instantiate(knightPrefab.gameObject, position, Quaternion.identity);
            KnightProduct newProduct = instance.GetComponent<KnightProduct>();
            newProduct.Initialize();
            return newProduct;
        }
    }
}
