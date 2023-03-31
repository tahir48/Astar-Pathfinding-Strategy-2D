using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using StrategyGame_2DPlatformer.Core;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class MilitaryBuilding : Building
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        private SoldierProduction _soldierProduction;
        public override string Name { get { return _name; } }
        public override int Cost { get { return _cost; } }
        public override int SizeX { get => _sizeX; protected set => _sizeX = value; }
        public override int SizeY { get => _sizeY; protected set => _sizeY = value; }
        private void Start()
        {
            _soldierProduction = GetComponent<SoldierProduction>();
        }

        public override void OnBuildingPlaced()
        {
            GameData.instance.SpendMoney(Cost);
            _soldierProduction.FindSpawnPoint();
        } 
    }
}
