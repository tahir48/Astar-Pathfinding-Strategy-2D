namespace StrategyGame_2DPlatformer.Contracts
{
    public interface IDamageable
    {
        int MaxHealth { get; }
        void Damage(int damage);
    }
}


