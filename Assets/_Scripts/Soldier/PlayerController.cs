using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TilemapController tilemapController;
    // Start is called before the first frame update
    void Start()
    {
        if (tilemapController == null) { FindObjectOfType<TilemapController>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.position = tilemapController.GetTilePositionAtMouseClick();
        }
    }
}
