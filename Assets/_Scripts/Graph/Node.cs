using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3Int Position;
    public List<Edge> Edges;

    public Node(Vector3Int position)
    {
        Position = position;
        Edges = new List<Edge>();
    }
}