using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class MilitaryBuilding : Building
    {
        public bool isPlaced;
        #region Selection Related Variables
        private Image buildingsImageUI;
        private Image soldierImageUI;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite nullImage;
        [SerializeField] private Sprite swordsmanSprite;
        private SpriteRenderer _spriteRender;
        bool isSelected = false;
        #endregion


        private void Start()
        {
            buildingsImageUI = GameObject.FindGameObjectWithTag("BuildingImage").GetComponent<Image>();
            soldierImageUI = GameObject.FindGameObjectWithTag("Soldiers").GetComponent<Image>();
            _spriteRender = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if (isSelected && Input.GetMouseButtonDown(0))
            {
                // Check if the clicked object is not the building itself
                if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
                {
                    // Deselect the building
                    isSelected = false;
                    _spriteRender.color = Color.white;
                    buildingsImageUI.sprite = nullImage;
                    soldierImageUI.sprite = nullImage;
                }
            }
        }

        bool IsClickOnBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return _spriteRender.bounds.Contains(mousePosition);
        }


        #region Selection Related Functionality
        public void OnBarracksClicked()
        {
            buildingsImageUI.sprite = sprite;
            soldierImageUI.sprite = swordsmanSprite;
        }

        void OnMouseDown()
        {
            if (isPlaced)
            {
                isSelected = true;
                _spriteRender.color = Color.red;
                OnBarracksClicked();
            }
        }
        #endregion

    }
}
