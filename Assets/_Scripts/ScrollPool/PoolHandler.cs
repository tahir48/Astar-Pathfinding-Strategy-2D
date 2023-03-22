using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace StrategyGame_2DPlatformer
{
    public class PoolHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
    {

        [SerializeField] private ScrollPool scrollPool;
        private float _bound;
        private ScrollRect myScrollRect;
        private Vector2 lastDragPosition; // Used to understand scroll direction
        private bool positiveDrag;


        private void Start()
        {
            _bound = 40;
            myScrollRect = GetComponent<ScrollRect>();
        }

        public Transform GetPooledObject(ScrollRect scrollRect, int index)
        {
            return scrollRect.content.GetChild(index);
        }

        public void OnBeginDrag(PointerEventData data) // Is called when the user drags on the scroll view
        {
            lastDragPosition = data.position;
        }

        public void OnDrag(PointerEventData data) // Is called when the user drags on the scroll view
        {

            positiveDrag = data.position.y > lastDragPosition.y;
            lastDragPosition = data.position;
        }


        public void OnScroll(PointerEventData data)
        {
            positiveDrag = data.scrollDelta.y > 0;
        }

        public void OnScrollDrag()
        {
            HandleScroll();
        }

        private void HandleScroll()
        {
            // Get the indices and elements of the first and last items in the scroll view
            int firstIndex = positiveDrag ? myScrollRect.content.childCount - 1 : 0;
            var firstElement = GetPooledObject(myScrollRect, firstIndex);
            int lastIndex = positiveDrag ? 0 : myScrollRect.content.childCount - 1;
            var lastElement = GetPooledObject(myScrollRect, lastIndex);

            var aimPosition = transform.position.y;
            if (positiveDrag)
            {
                aimPosition = transform.position.y + scrollPool.ChildHeight * 0.5f + _bound;
                if (!(firstElement.position.y - scrollPool.ChildHeight * 0.5f > aimPosition))
                {
                    return;
                }
            }
            else
            {
                aimPosition = transform.position.y - scrollPool.ChildHeight * 0.5f - _bound;
                if (!(firstElement.position.y + scrollPool.ChildHeight * 0.5f < aimPosition))
                {
                    return;
                }
            }

            Vector2 newPos = RecalculatePoolPositions(lastElement);
            UpdatePool(firstElement, newPos, lastIndex);
        }

        [SerializeField] int _margin;
        private Vector2 RecalculatePoolPositions(Transform lastElement)
        {
            Vector2 newPos = lastElement.position;
            if (positiveDrag)
            {
                newPos.y = lastElement.position.y - scrollPool.ChildHeight * 1.5f + _margin;
            }
            else
            {
                newPos.y = lastElement.position.y + scrollPool.ChildHeight * 1.5f - _margin;
            }
            return newPos;
        }

        private void UpdatePool(Transform firstElement, Vector2 newPos, int lastIndex)
        {
            firstElement.position = newPos;
            firstElement.SetSiblingIndex(lastIndex);
        }

    }
}