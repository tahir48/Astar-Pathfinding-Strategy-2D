using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.UI;
using StrategyGame_2DPlatformer.SoldierFactory;
using UnityEngine;
using System;
using StrategyGame_2DPlatformer.Soldiers;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class MilitaryBuilding : Building
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        public override string Name { get { return _name; } }
        public override int Cost { get { return _cost; } }

        #region Production Related Variables
        public enum FactoryStates { Swordsman, Spearman, Knight }
        public FactoryStates factoryState;

        private Factory _currentFactory;
        private Vector3Int _spawnpoint;
        #endregion

        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; protected set => _sizeX = value; }
        public override int SizeY { get => _sizeY; protected set => _sizeY = value; }
        #endregion
        private Tilemap tilemap;
        private Graph graph;
        #region Production Related Functionality
        private void FindSpawnPoint()
        {
            Vector3Int pos = FindCorner();
            var isRightSideOpen = pos != null && !graph.GetNodeAtPosition(pos + Vector3Int.right).isOccupied;
            if (isRightSideOpen)
            {
                _spawnpoint = pos + Vector3Int.right;
            }
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

        private void Start()
        {
            tilemap = GameData.instance.Tilemap;
            graph = GameData.instance.Graph;
        }

        private void OnEnable()
        {
            SpawnEvent.onSpawnButtonClick += HandleButtonClick;
            ChangeFactoryState(FactoryStates.Swordsman);
        }

        private void OnDisable()
        {
            SpawnEvent.onSpawnButtonClick -= HandleButtonClick;
        }

        public Vector3Int GetClosestNodeToAttack(Soldier soldier)
        {
            Vector3Int soldierPosition = new Vector3Int(soldier.currentNode.x, soldier.currentNode.y, 0);
            Vector3Int closestPosition = soldierPosition;
            //TileBase closestTile = null;
            float closestDistance = float.MaxValue;

            foreach (Vector3Int occupiedPosition in OccupiedPositions)
            {
                Vector3 position = tilemap.CellToWorld(occupiedPosition);
                float distance = Vector3.Distance(position, soldierPosition);

                if (distance < closestDistance)
                {
                    closestPosition = tilemap.WorldToCell(position);
                    closestDistance = distance;
                }
            }
            Vector3Int attackPoint = Vector3Int.right;
            if (soldierPosition.x < closestPosition.x)
            {
                attackPoint = closestPosition + Vector3Int.left;
            }
            else if (soldierPosition.x > closestPosition.x)
            {
                attackPoint = closestPosition + Vector3Int.right;
            }
            else if (soldierPosition.y < closestPosition.y)
            {
                attackPoint = closestPosition + Vector3Int.down;
            }
            else
            {
                attackPoint = closestPosition + Vector3Int.up;
            }

            return attackPoint;
        }

        private void HandleButtonClick(string soldierName)
        {
            if (!GetComponent<SelectableBuilding>().IsSelected) return;
            if (GameData.instance.AvailaiblePopulation - GameData.instance.CurrentPopulation <= 0) return;
            FindSpawnPoint();
            Vector3 spawnPoint = tilemap.GetCellCenterWorld(_spawnpoint);
            if (Enum.TryParse(soldierName, out factoryState))
            {
                ChangeFactoryState(factoryState);
            }

            if (spawnPoint != null && soldierName != null)
            {
                _currentFactory?.GetProduct(spawnPoint);
            }
        }
        public void ChangeFactoryState(FactoryStates state)
        {
            factoryState = state;
            Debug.Log("Factory state has been " + state);
            switch (state)
            {
                case FactoryStates.Swordsman:
                    _currentFactory = GameData.instance.swordsmanFactory;
                    break;
                case FactoryStates.Spearman:
                    _currentFactory = GameData.instance.spearmanFactory;
                    break;
                case FactoryStates.Knight:
                    _currentFactory = GameData.instance.knightFactory;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Placement Related Functionality
        public override void OnBuildingPlaced()
        {
            GameData.instance.SpendMoney(Cost);
        }
        #endregion

    }
}
