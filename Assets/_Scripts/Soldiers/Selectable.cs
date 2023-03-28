using StrategyGame_2DPlatformer.Contracts;
using StrategyGame_2DPlatformer.Soldiers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StrategyGame_2DPlatformer
{
    public class Selectable : MonoBehaviour, ISelectable
    {
        private bool _isSelected;
        private SpriteRenderer _spriteRenderer;
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }

        private void Start()
        {
            _isSelected = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnMouseDown()
        {
            OnSelected();
        }

        public void OnSelected()
        {
            _isSelected = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            GameManagement.GameData.instance.soldier = GetComponent<Soldier>();
        }

        public void OnDeselected()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !IsClickOnBuilding())
            {
                _isSelected = false;
                _spriteRenderer.color = Color.white;
            }
        }

        private bool IsClickOnBuilding()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return GetComponent<SpriteRenderer>().bounds.Contains(mousePosition);
        }

        void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                OnDeselected();
            }
        }
    }
}
