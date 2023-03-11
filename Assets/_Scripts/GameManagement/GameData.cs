using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameData : MonoBehaviour
{
    #region Simple Sington
    public static GameData instance;
    #endregion

    #region Graph Related Data
    private Graph _graph;
    [SerializeField] private Tilemap _tilemap;
    public Graph Graph { get { return _graph; } private set { } }
    public Tilemap Tilemap { get { return _tilemap; } private set { } }
    #endregion
    private void Awake()
    {
        #region Simple Sington
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        #region Graph Related Data
        _graph = new Graph();
        _graph.CreateGraphFromTilemap(_tilemap);
        #endregion
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
