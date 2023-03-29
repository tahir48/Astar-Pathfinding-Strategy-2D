using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class PopulationBuilding : Building
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        public override string Name { get { return _name; } }
        public override int Cost { get { return _cost; } }

        #region Damage Related Variables
        public Vector3Int takeDamageFrom;

        #endregion
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion
        private GameData _gameData;


        private void OnEnable()
        {
            _gameData = GameData.instance;
        }

        private void OnDisable()
        {
            _gameData.DecreaseCurrentAvailaiblePop(5);
        }

        #region Placement Related Functionality
        public override void OnBuildingPlaced()
        {
            _gameData.IncreaseCurrentAvailaiblePop(5);
            _gameData.SpendMoney(Cost);
        }
        #endregion
    }
}