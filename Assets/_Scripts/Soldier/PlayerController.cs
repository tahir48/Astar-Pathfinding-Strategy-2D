using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TilemapController tilemapController;
    public Tilemap tilemap;
    public float moveSpeed = 5f;

    private List<Node> _pathToWalk;
    private int _indexToVisit;
    private bool isMoving;
    private Node currentNode;

    void Start()
    {
        transform.position = new Vector3(GameData.instance.Graph.Nodes[0].x, GameData.instance.Graph.Nodes[0].y, 0);
        currentNode = GameData.instance.Graph.Nodes[0];
        _pathToWalk = new List<Node>();
        if (tilemapController == null) { FindObjectOfType<TilemapController>(); }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Node nextNode;
            nextNode = GameData.instance.Graph.GetNodeAtMouseClick(tilemap, Camera.main, GameData.instance.Graph.Nodes);
            _pathToWalk = AStar.FindPath(currentNode, nextNode);
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
                currentNode = _pathToWalk[_indexToVisit];
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


}

