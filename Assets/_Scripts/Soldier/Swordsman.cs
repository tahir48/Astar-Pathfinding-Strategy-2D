using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Swordsman : Soldier
{
    
    #region Movement Related Variables
    private List<Node> _pathToWalk;
    private float moveSpeed = 2f;
    private int _indexToVisit;
    private bool isMoving;
    #endregion
    private void Start()
    {
        currentNode = GameData.instance.Graph.Nodes[0];
        transform.position = new Vector3(GameData.instance.Graph.Nodes[0].x, GameData.instance.Graph.Nodes[0].y, 0);
    }

    void Update()
    {
        #region Movement Related Functionality
        if (Input.GetMouseButtonDown(1))
        {
            Node nextNode;
            nextNode = GameData.instance.Graph.GetNodeAtMouseClick(GameData.instance.Tilemap, Camera.main, GameData.instance.Graph.Nodes);
            _pathToWalk = AStar.FindPath(currentNode, nextNode);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _indexToVisit = 0;
            isMoving = true;
        }
        if (isMoving)
        {
            Move();
        }
        #endregion
    }

    #region Movement Related Functionality
    public override void Move()
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
    #endregion
}
