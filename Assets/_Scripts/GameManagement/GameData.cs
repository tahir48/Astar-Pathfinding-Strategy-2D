using UnityEngine;
using UnityEngine.Tilemaps;
namespace StrategyGame_2DPlatformer.GameManagement
{
    public class GameData : MonoBehaviour
    {
        public GameObject swordsmanPrefab;

        #region Population Related Variables
        private int _currentPopulationSize;
        private int _maxPopulationSize;
        public int CurrentPopulationSize { get { return _currentPopulationSize; } set { _currentPopulationSize = value; } }
        public int MaxPopulationSize { get { return _maxPopulationSize; } private set { } }
        #endregion

        #region Simple Sington
        public static GameData instance;
        #endregion

        #region Graph Related Data
        private Graph _graph;
        [SerializeField] private Tilemap _tilemap;
        public Graph Graph { get { return _graph; } private set { } }
        public Tilemap Tilemap { get { return _tilemap; } private set { } }
        #endregion

        #region Gameplay Related Data
        private int _maxPopulation;
        private int _currentPopulation;
        public int MaxPopulation { get { return _maxPopulation; } private set { } }
        public int CurrentPopulation { get { return _currentPopulation; } set { _currentPopulation = value; } }

        #endregion
        private void Awake()
        {
            #region Population Related Variables
            _currentPopulationSize = 5;
            _maxPopulationSize = 200;
            #endregion
            #region Simple Sington
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion
            #region Graph Related Data
            _graph = new Graph();
            _graph.CreateGraphFromTilemap(_tilemap);
            #endregion
        }
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Draw Graph OnGizmos
        private void OnDrawGizmos()
        {
            if (_graph != null)
            {
                foreach (Node node in _graph.Nodes)
                {
                    if (node != null)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(new Vector3(node.x, node.y, 0), 0.1f);

                        foreach (Edge edge in node.edges)
                        {
                            if (edge.endNode != null)
                            {
                                Gizmos.color = Color.green;
                                Gizmos.DrawLine(new Vector3(node.x, node.y, 0), new Vector3(edge.endNode.x, edge.endNode.y, 0));
                            }
                        }
                    }
                }
            }
        }
        #endregion

    }
}
