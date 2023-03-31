using System.Collections.Generic;

namespace StrategyGame_2DPlatformer.GraphStructure
{
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

        public void SetOccupied(bool isOccupied)
        {
            this.isOccupied = isOccupied;
        }

    }
}