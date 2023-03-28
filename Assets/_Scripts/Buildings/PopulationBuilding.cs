using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;

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
        public override Vector3Int SpawnPoint
        {
            get
            {
                if (takeDamageFrom != null)
                {
                    return takeDamageFrom;
                }
                else
                {
                    return FindSpawnPoint();
                }
            }
        }
        #endregion
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion



        private void OnDisable()
        {
            GameData.instance.DecreaseCurrentAvailaiblePop(5);
        }

        #region Damage Related Functionality
        public Vector3Int FindSpawnPoint()
        {
            Vector3Int pos = FindCorner();
            var isRightSideOpen = pos != null && !GameManagement.GameData.instance.Graph.GetNodeAtPosition(pos + Vector3Int.right).isOccupied;
            if (isRightSideOpen)
            {
                takeDamageFrom = pos + Vector3Int.right;
            }
            return takeDamageFrom;
        }
        private Vector3Int FindCorner()
        {
            Vector3Int corner = OccupiedPositions[0];
            foreach (Vector3Int pos in OccupiedPositions)
            {
                if (pos.x > corner.x) corner.x = pos.x;
                if (pos.y > corner.y) corner.y = pos.y;
            }
            return corner;
        }
        #endregion

        #region Placement Related Functionality
        public override void OnBuildingPlaced()
        {
            GameData.instance.IncreaseCurrentAvailaiblePop(5);
            GameData.instance.SpendMoney(5);
        }
        #endregion
    }
}