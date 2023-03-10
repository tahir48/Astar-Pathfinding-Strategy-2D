using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TilemapController tilemapController;
    public Tilemap tilemap;
    private Graph _graph;
    public Graph Graph { get { return _graph; } private set { } }
    public float moveSpeed = 5f;

    private List<Node> _pathToWalk;
    private int _indexToVisit;
    private bool isMoving;

    private void Awake()
    {
        _graph = new Graph();
        _graph.CreateGraphFromTilemap(tilemap);
        transform.position = new Vector3(_graph.Nodes[0].x, _graph.Nodes[0].y, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        _pathToWalk = new List<Node>();
        if (tilemapController == null) { FindObjectOfType<TilemapController>(); }
        _pathToWalk.Add(_graph.Nodes[0]);
        _pathToWalk.Add(_graph.Nodes[5]);
        _pathToWalk.Add(_graph.Nodes[15]);
        _pathToWalk.Add(_graph.Nodes[28]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //transform.position = tilemapController.GetTilePositionAtMouseClick();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _indexToVisit = 0;
            isMoving = true;
        }
        if (isMoving)
        {
            Vector3 targetPosition = new Vector3(_pathToWalk[_indexToVisit].x, _pathToWalk[_indexToVisit].y, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                _indexToVisit++;
                if (_indexToVisit >= _pathToWalk.Count)
                {
                    isMoving = false;
                }
            }
        }
    }

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


}

