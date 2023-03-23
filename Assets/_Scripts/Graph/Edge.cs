public class Edge
{
    public Node startNode;
    public Node endNode;
    public float weight;

    public Edge(Node startNode, Node endNode, float weight = 1f)
    {
        this.startNode = startNode;
        this.endNode = endNode;
        this.weight = weight;
    }
}
