using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class ProductionBuilding : Building
    {
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion
        public override void OnDeselected()
        {
            throw new System.NotImplementedException();
        }

        public override void OnSelected()
        {
            throw new System.NotImplementedException();
        }
    }
}
