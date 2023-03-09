using UnityEngine;
using UnityEngine.Tilemaps;

public class TrialGraphScript : MonoBehaviour
{
    Graph graph;
    [SerializeField] Tilemap tilemap;

    private void Start()
    {
        graph = new Graph();
        graph.CreateGraphFromTilemap(tilemap);
        foreach (var item in graph.Nodes)
        {
            Debug.Log("graph.Nodes  " + graph.Nodes);
        }
    }

}
