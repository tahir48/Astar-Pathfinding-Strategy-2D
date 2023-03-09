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
        foreach (var item in _graph.Nodes)
        {
            Debug.Log(item.Position);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        _pathToWalk = new List<Node>();
        if (tilemapController == null) { FindObjectOfType<TilemapController>(); }
        _pathToWalk.Add(_graph.Nodes[10]);
        _pathToWalk.Add(_graph.Nodes[8]);
        _pathToWalk.Add(_graph.Nodes[15]);
        _pathToWalk.Add(_graph.Nodes[32]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.position = tilemapController.GetTilePositionAtMouseClick();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _indexToVisit = 0;
            isMoving = true;
        }
        if (isMoving)
        {
            Vector3 targetPosition = _pathToWalk[_indexToVisit].Position;
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
}

