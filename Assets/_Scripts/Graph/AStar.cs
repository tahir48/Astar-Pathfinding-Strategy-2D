using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public static List<Node> FindPath(Node startNode, Node endNode)
    {
        List<Node> openList = new List<Node>(); //Nodes that have been discovered but not yet evaluated
        List<Node> closedList = new List<Node>(); //Nodes that have been evaluated.
        List<Node> path = new List<Node>();

        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        Dictionary<Node, float> fScore = new Dictionary<Node, float>();
        Dictionary<Node, Node> coupledVisits = new Dictionary<Node, Node>();

        openList.Add(startNode);
        gScore[startNode] = 0;
        fScore[startNode] = HCost(startNode, endNode);


        while (openList.Count > 0)
        {
            Node current = GetLowestFScoreNode(openList, fScore);
            //
            //Edge cases for current node? *** check ***
            //
            if (current == endNode)
            {
                //List<Node> path = new List<Node>();
                path.Add(endNode);
                Node tmp = endNode;
                while (coupledVisits.ContainsKey(tmp))
                {
                    tmp = coupledVisits[tmp]; //equivalent to tmp = tmp.prev
                    path.Insert(0, tmp); //insert at the begining of the list
                    Debug.Log(tmp.x + " inserted at the begining of the path list");
                }
                return path;
            }

            openList.Remove(current);
            closedList.Add(current);
            foreach (var edge in current.edges)
            {
                Node neighbor = edge.startNode == current ? edge.endNode : edge.startNode;
                if (closedList.Contains(neighbor)) continue;
                float tempGScore = gScore[current] + edge.weight;
                if (!openList.Contains(neighbor) || tempGScore < gScore[neighbor])
                {
                    coupledVisits[neighbor] = current;
                    gScore[neighbor] = tempGScore;
                    fScore[neighbor] = gScore[neighbor] + HCost(neighbor, endNode);
                    openList.Add(neighbor);
                }
            }
        }
        return null;
    }

    private static Node GetLowestFScoreNode(List<Node> nodeList, Dictionary<Node, float> fScore)
    {   //We start with the assumption that the first element of the list has the lowest fscore.
        //Then we traverse through the list to find another node with a lower fscore.
        //If we find one, we assign it as the node with the lowest fscore.
        Node lowestNode = nodeList[0];
        float lowestFScore = fScore[lowestNode];
        foreach (Node node in nodeList)
        {
            float nodeFScore = fScore[node];
            if (nodeFScore < lowestFScore)
            {
                lowestNode = node;
                lowestFScore = nodeFScore;
            }
        }
        return lowestNode;
    }


    private static float HCost(Node startNode, Node endNode)
    {
        return Mathf.Sqrt(Mathf.Pow(endNode.x - startNode.x, 2) + Mathf.Pow(endNode.y - startNode.y, 2));
    }

}