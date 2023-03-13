using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class MilitaryBuilding : Building
    {
        public bool isPlaced;
        #region Selection Related Variables
        [SerializeField] private Image barracksImage;
        [SerializeField] private Image soldierImage;
        [SerializeField] private Sprite sprite;
        bool isSelected = false;
        #endregion

        #region Selection Related Functionality
        public void OnBarracksClicked()
        {
            barracksImage = GameObject.FindGameObjectWithTag("BuildingImage").GetComponent<Image>();
            barracksImage.sprite = sprite;
        }

        void OnMouseDown()
        {
            if (isPlaced)
            {
                isSelected = true;
                GetComponent<SpriteRenderer>().color = Color.red;
                OnBarracksClicked();
            }
        }
        #endregion

    }
}
