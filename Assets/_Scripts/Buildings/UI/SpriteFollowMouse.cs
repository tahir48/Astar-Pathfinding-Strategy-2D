using UnityEngine;

namespace StrategyGame_2DPlatformer.Buildings
{
    public class SpriteFollowMouse : MonoBehaviour
    {
        [SerializeField] private Vector2 _offSet;
        // This method is used when a building image is selected from the Infinite Scroll
        // Until placed to an availaible position
        void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + _offSet.x, mousePos.y + _offSet.y, 0f);
        }
    }
}