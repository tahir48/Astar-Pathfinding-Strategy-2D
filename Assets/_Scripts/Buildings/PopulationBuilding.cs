using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace StrategyGame_2DPlatformer
{
    public class PopulationBuilding : Building
    {
        #region Damage Related Variables
        private int _currentHealth;
        [SerializeField] private int _maxHealth;
        public override int MaxHealth { get { return _maxHealth; } }
        [SerializeField] private Image _fillBar;

        #endregion

        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion

        private SpriteRenderer _spriteRenderer;
        private void OnEnable()
        {
            _currentHealth = MaxHealth;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        void Update()
        {
            #region Selection Related Functionality
            if (IsSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }
            #endregion
        }

        public override void OnSelected()
        {
            IsSelected = true;
            _spriteRenderer.color = Color.red;
            Debug.Log("Ekstra functionality");
            GameData.instance.ShowInformationMenu(); //I will possibly use Coroutine here
        }

        public void OnHouseClicked()
        {
            GameData.instance.ShowInformationMenu();
            GameData.instance.buildingsImageUI.sprite = GameData.instance.productionBuildingSprite;
            //soldierHolder.gameObject.SetActive(true);
        }
        void OnMouseDown()
        {
            if (IsPlaced)
            {
                OnSelected();
            }
        }

        public override void OnDeselected()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
            {
                // Deselect the building
                IsSelected = false;
                _spriteRenderer.color = Color.white;
                GameData.instance.HideInformationMenu();
            }
        }

        bool IsClickOnBuilding()
        {
            // Check if the mouse position is inside the building sprite
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return _spriteRenderer.bounds.Contains(mousePosition);
        }

        #region Damage related functionality
        public override void Damage(int damage)
        {
            _currentHealth -= damage;
            _fillBar.fillAmount = ((float)_currentHealth / (float)_maxHealth);
        }
        #endregion

    }
}
