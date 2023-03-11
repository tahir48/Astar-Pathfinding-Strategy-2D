using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tilemap;
    public TileBase[] tiles; // An array of Tiles in the Tileset
    [SerializeField] private GameObject _player;
    Vector3 nextposition;



    void Start()
    {
        mainCamera = Camera.main;
        BoundsInt bounds = tilemap.cellBounds;
        tiles = new TileBase[bounds.size.x * bounds.size.y * bounds.size.z];
        for (int z = bounds.min.z; z < bounds.max.z; z++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, z));
                    int index = (z - bounds.min.z) * bounds.size.x * bounds.size.y + (y - bounds.min.y) * bounds.size.x + (x - bounds.min.x);
                    tiles[index] = tile;
                }
            }
        }
    }


    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ChangeColorOfTileAtMouseClick();
        //}
    }
    public TileBase GetTileByIndex(int index)
    {
        // Check that the index is within the range of the tiles array
        if (index >= 0 && index < tiles.Length)
        {
            return tiles[index];
        }
        else
        {
            Debug.LogError("Invalid tile index: " + index);
            return null;
        }
    }


    public void ChangeColorOfTileAtMouseClick()
    {
        Vector3 tilePosToChangeColor = GetTilePositionAtMouseClick();
        Vector3Int tilePosition = tilemap.WorldToCell(tilePosToChangeColor);
        tilemap.SetTileFlags(tilePosition, TileFlags.None);
        tilemap.SetColor(tilePosition, Color.red);
    }

    public Vector3 GetTilePositionAtMouseClick()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 nextposition = mainCamera.ScreenToWorldPoint(mousePosScreen);
        Vector3Int nextTilePosition = tilemap.WorldToCell(nextposition);
        Vector3 destination = tilemap.GetCellCenterWorld(nextTilePosition);
        return destination;
    }

    public TileBase GetTileAtPosition(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);
        return tile; 
    }


}