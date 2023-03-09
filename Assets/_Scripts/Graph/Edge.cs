using UnityEngine;

public class Edge
{
    public Node StartNode;
    public Node EndNode;
    public float Weight;

    public Edge(Node startNode, Node endNode, float weight)
    {
        StartNode = startNode;
        EndNode = endNode;
        Weight = weight;
    }
}
