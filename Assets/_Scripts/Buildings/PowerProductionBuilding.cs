using UnityEngine;
using StrategyGame_2DPlatformer.GameManagement;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class PowerProductionBuilding : Building
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        public override string Name { get { return _name; } }
        public override int Cost { get { return _cost; } }

        bool generate = false;
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion

        #region Generate Money
        float durationPassed = 0;
        float durationToPass = 1;
        private GameData _gameData;
        private void OnEnable()
        {
            _gameData = GameData.instance;
        }

        private void Update()
        {
            if (generate)
            {
                //Generate money per "durationToPass" seconds
                if (durationPassed > durationToPass)
                {
                    _gameData.IncreaseMoney();
                    durationPassed = 0;
                }
                durationPassed += Time.deltaTime;
            }
            #endregion
        }

        #region Placement Related Functionality
        public override void OnBuildingPlaced()
        {
            generate = true;
            _gameData.SpendMoney(Cost);
        }
        #endregion
    }
}
