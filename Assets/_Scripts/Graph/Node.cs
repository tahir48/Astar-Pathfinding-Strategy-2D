using System.Collections.Generic;

public class Node
{
    public int x;
    public int y;
    public bool isOccupied;
    public List<Edge> edges;

    public Node(int x, int y, bool isOccupied = false)
    {
        this.x = x;
        this.y = y;
        edges = new List<Edge>();
        this.isOccupied = isOccupied;
    }
}