using UnityEngine;
using UnityEngine.Tilemaps;
namespace StrategyGame_2DPlatformer.GameManagement
{
    public class GameData : MonoBehaviour
    {
        #region Simple Sington
        public static GameData instance;
        #endregion

        #region Graph Related Data
        private Graph _graph;
        [SerializeField] private Tilemap _tilemap;
        public Graph Graph { get { return _graph; } private set { } }
        public Tilemap Tilemap { get { return _tilemap; } private set { } }
        #endregion
        private void Awake()
        {
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
            if (GameData.instance.Graph != null)
            {
                foreach (Node node in GameData.instance.Graph.Nodes)
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
