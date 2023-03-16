namespace StrategyGame_2DPlatformer.Contracts
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        void OnSelected();
        void OnDeselected();
    }
}
