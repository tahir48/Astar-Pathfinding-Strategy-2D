using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class SpriteFollowMouse : MonoBehaviour
    {
        void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
    }
}