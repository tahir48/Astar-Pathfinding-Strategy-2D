using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class BarrackPresenter : MonoBehaviour
    {
        public GameObject spritePrefab;
        public int spriteWidth = 4;
        public int spriteHeight = 4;
        private bool isOpen;
        GameObject sprite;
        private void Start()
        {
            isOpen = false;
        }

        private void Update()
        {
            if (isOpen && Input.GetMouseButtonDown(1)) { 
                Destroy(sprite); 
                isOpen = false; 
            }
        }
        public void OnBarracksButtonClick()
        {
            sprite = Instantiate(spritePrefab, Vector3.zero, Quaternion.identity);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sprite.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            isOpen = true;
        }


    }
}
