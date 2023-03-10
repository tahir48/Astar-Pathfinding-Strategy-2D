using System.Collections.Generic;
using UnityEngine;



public class AStar
{
    public List<Node> FindPath(Node startNode, Node endNode)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> path = new List<Node>();

        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        Dictionary<Node, float> fScore = new Dictionary<Node, float>();

        openList.Add(startNode);
        gScore[startNode] = 0;
        fScore[startNode] = Cost(startNode, endNode);


        while (openList.Count > 0)
        {

        }
        return path;

    }

    private float Cost(Node startNode, Node endNode)
    {
        return Mathf.Sqrt(Mathf.Pow(endNode.x - startNode.x, 2) + Mathf.Pow(endNode.y - startNode.y, 2));
    }

}