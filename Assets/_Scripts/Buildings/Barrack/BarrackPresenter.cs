using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class BarrackPresenter : MonoBehaviour
    {
        public GameObject spritePrefab;
        public int spriteWidth = 4;
        public int spriteHeight = 4;
        private void Start()
        {
        }
        public void OnBarracksButtonClick()
        {
            GameObject sprite = Instantiate(spritePrefab, Vector3.zero, Quaternion.identity);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sprite.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }


    }
}
