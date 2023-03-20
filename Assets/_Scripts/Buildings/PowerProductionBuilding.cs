using UnityEngine;
using UnityEngine.UI;
using StrategyGame_2DPlatformer.GameManagement;


namespace StrategyGame_2DPlatformer
{
    public class PowerProductionBuilding : Building
    {
        #region Damage Related Variables
        private int _currentHealth;
        [SerializeField] private int _maxHealth;
        public override int MaxHealth { get { return _maxHealth; }}
        [SerializeField] private Image _fillBar;
        #endregion

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }
        #region Placement Related Variables
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        public override int SizeX { get => _sizeX; set => _sizeX = value; }
        public override int SizeY { get => _sizeY; set => _sizeY = value; }
        #endregion
        private SpriteRenderer _spriteRenderer;
        public override void OnDeselected()
        {
            GameData.instance.HideInformationMenu(); //I will possibly use Coroutine here
        }

        public override void OnSelected()
        {
            IsSelected = true;
            _spriteRenderer.color = Color.red;
            Debug.Log("Ekstra functionality");
            GameData.instance.ShowInformationMenu(); //I will possibly use Coroutine here
        }

        public void OnMillClicked()
        {
            GameData.instance.buildingsImageUI.sprite = GameData.instance.productionBuildingSprite;
            GameData.instance.ShowInformationMenu();
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
