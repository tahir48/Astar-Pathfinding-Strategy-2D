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
            Node current = openList[0]; //assume it has the lowest Fscore, need a function to get lowest fscored node from the openlist.
            openList.Remove(current);
            closedList.Add(current);
            foreach (var item in current.edges)
            {
                Node neighbor = item.endNode; //not always the case, fix it.
                //find gscore, find the neighbor with lowest gscore, and assign to it openlist.
                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
            }
        }
        return path;

    }

    private Node GetLowestFScoreNode(List<Node> nodeList, Dictionary<Node, float> fScore)
    {
        Node lowestFscoreNode = nodeList[0];
        //Fill
        return lowestFscoreNode;
    }


    private float Cost(Node startNode, Node endNode)
    {
        return Mathf.Sqrt(Mathf.Pow(endNode.x - startNode.x, 2) + Mathf.Pow(endNode.y - startNode.y, 2));
    }

}