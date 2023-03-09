using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Graph
{
    public List<Node> Nodes;

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public void CreateGraphFromTilemap(Tilemap tilemap)
    {
        Grid grid = tilemap.layoutGrid;

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                Node node = new Node(position);
                Nodes.Add(node);

                // Check for adjacent tiles and add edges
                Vector3Int leftPosition = position + Vector3Int.left;
                if (tilemap.HasTile(leftPosition))
                {
                    TileBase leftTile = tilemap.GetTile(leftPosition);
                    float weight = GetWeight(tile) + GetWeight(leftTile);
                    Node leftNode = Nodes.Find(n => n.Position == leftPosition);
                    Edge edge = new Edge(node, leftNode, weight);
                    node.Edges.Add(edge);
                }

                Vector3Int rightPosition = position + Vector3Int.right;
                if (tilemap.HasTile(rightPosition))
                {
                    TileBase rightTile = tilemap.GetTile(rightPosition);
                    float weight = GetWeight(tile) + GetWeight(rightTile);
                    Node rightNode = Nodes.Find(n => n.Position == rightPosition);
                    Edge edge = new Edge(node, rightNode, weight);
                    node.Edges.Add(edge);
                }

                Vector3Int downPosition = position + Vector3Int.down;
                if (tilemap.HasTile(downPosition))
                {
                    TileBase downTile = tilemap.GetTile(downPosition);
                    float weight = GetWeight(tile) + GetWeight(downTile);
                    Node downNode = Nodes.Find(n => n.Position == downPosition);
                    Edge edge = new Edge(node, downNode, weight);
                    node.Edges.Add(edge);
                }

                Vector3Int upPosition = position + Vector3Int.up;
                if (tilemap.HasTile(upPosition))
                {
                    TileBase upTile = tilemap.GetTile(upPosition);
                    float weight = GetWeight(tile) + GetWeight(upTile);
                    Node upNode = Nodes.Find(n => n.Position == upPosition);
                    Edge edge = new Edge(node, upNode, weight);
                    node.Edges.Add(edge);
                }
            }
        }

    }


    private float GetWeight(TileBase tile)
    {
        // Add logic here to determine the weight of a tile
        return 1f;
    }
}

    