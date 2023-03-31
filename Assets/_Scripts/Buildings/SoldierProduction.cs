using StrategyGame_2DPlatformer.Core;
using StrategyGame_2DPlatformer.GameManagement;
using StrategyGame_2DPlatformer.GraphStructure;
using StrategyGame_2DPlatformer.UI;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class SoldierProduction : MonoBehaviour
    {
        private Vector3Int _spawnpoint;
        public enum FactoryStates { Swordsman, Spearman, Knight }
        public FactoryStates factoryState;
        private Tilemap tilemap;
        private Graph graph;
        private Factory _currentFactory;
        private Building _building;

        void Start()
        {
            _building = GetComponent<Building>();
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

        public void FindSpawnPoint()
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
            Vector3Int corner = _building.OccupiedPositions[0];
            foreach (Vector3Int pos in _building.OccupiedPositions)
            {
                if (pos.x > corner.x) corner.x = pos.x;
                if (pos.y > corner.y) corner.y = pos.y;
            }
            return corner;
        }

        private void HandleButtonClick(string soldierName)
        {
            if (!GetComponent<SelectableBuilding>().IsSelected) return;
            if (GameData.instance.AvailaiblePopulation - GameData.instance.CurrentPopulation <= 0) return;
            if (GameData.instance.Money < 20) return;
            if (_spawnpoint == null) FindSpawnPoint();
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
    }
}
