using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTrialScript : MonoBehaviour
{
    private Tilemap tilemap;
    private Tile tile;
    private Color originalColor;
    private bool playerOnTile = false;

    void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        tile = tilemap.GetTile<Tile>(tilemap.WorldToCell(transform.position));
        originalColor = tile.color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("tilemap  " + tilemap.name);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerOnTile = true;
            tile.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerOnTile = false;
            tile.color = originalColor;
        }
    }
}