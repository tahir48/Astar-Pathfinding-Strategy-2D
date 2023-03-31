namespace StrategyGame_2DPlatformer.Core { 
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        void OnSelected();
        void OnDeselected();
    }
}
