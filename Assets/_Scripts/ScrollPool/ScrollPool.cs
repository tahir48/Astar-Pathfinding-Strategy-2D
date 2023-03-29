using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class ScrollPool : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
    {
        [SerializeField] private PooledObjects _content;
        [SerializeField] private float _boundary;
        private ScrollRect myScrollRect;
        private Vector2 lastDragPosition;
        private bool positiveDrag;

        private void Start()
        {
            myScrollRect = GetComponent<ScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastDragPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            positiveDrag = eventData.position.y > lastDragPosition.y;
            lastDragPosition = eventData.position;
        }

        public void OnScroll(PointerEventData eventData)
        {
            positiveDrag = eventData.scrollDelta.y > 0;
        }

        public void OnViewScroll()
        {
            HandleScroll();
        }

        private void HandleScroll()
        {
            int firstIndex = positiveDrag ? myScrollRect.content.childCount - 1 : 0;
            var firstElement = GetPooledObject(myScrollRect, firstIndex);
            int lastIndex = positiveDrag ? 0 : myScrollRect.content.childCount - 1;
            var lastElement = GetPooledObject(myScrollRect, lastIndex);
            var aimPosition = transform.position.y;
            if (positiveDrag)
            {
                aimPosition = transform.position.y + _content.ChildHeight * 0.5f + _content.ItemSpacing;
                if (!(firstElement.position.y - _content.ChildHeight * 0.5f > aimPosition))
                {
                    return;
                }
            }
            else
            {
                aimPosition = transform.position.y - _content.ChildHeight * 0.5f - _content.ItemSpacing;
                if (!(firstElement.position.y + _content.ChildHeight * 0.5f < aimPosition))
                {
                    return;
                }
            }
            Vector2 newPos = RecalculatePoolPositions(lastElement);
            UpdatePool(firstElement, newPos, lastIndex);
        }

        private Vector2 RecalculatePoolPositions(Transform lastElement)
        {
            Vector2 newPos = lastElement.position;
            var screenIndependencyRatio = Screen.height / (7 * _content.ChildHeight);
            if (positiveDrag)
            {
                newPos.y = lastElement.position.y - _content.ChildHeight * screenIndependencyRatio + _content.ItemSpacing;
            }
            else
            {
                newPos.y = lastElement.position.y + _content.ChildHeight * screenIndependencyRatio - _content.ItemSpacing;
            }
            return newPos;
        }

        private void UpdatePool(Transform firstElement, Vector2 newPos, int lastIndex)
        {
            firstElement.position = newPos;
            firstElement.SetSiblingIndex(lastIndex);
        }

        public Transform GetPooledObject(ScrollRect scrollRect, int index)
        {
            return scrollRect.content.GetChild(index);
        }

    }
}