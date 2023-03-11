using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public List<Edge> edges;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        edges = new List<Edge>();
    }
}