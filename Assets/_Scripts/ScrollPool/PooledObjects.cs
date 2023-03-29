using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class PooledObjects : MonoBehaviour
    {
        
        [SerializeField] GameObject[] objectsToPoolArray;
        [SerializeField] private uint initPoolSize;
        private RectTransform[] rtPool;
        public uint InitPoolSize => initPoolSize;
        public float ItemSpacing { get { return itemSpacing; } }
        public float VerticalMargin { get { return verticalMargin; } }
        public float Height { get { return height; } }
        public float ChildHeight { get { return childHeight; } }
        private RectTransform rectTransform;
        private RectTransform[] rtChildren;
        private float height;
        private float childHeight;
        [SerializeField] private float itemSpacing;
        [SerializeField] private float verticalMargin;



        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            SetupPool();
        }

        private void Start()
        {
            
            rtChildren = new RectTransform[rectTransform.childCount];

            for (int i = 0; i < rectTransform.childCount; i++)
            {
                rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
            }

            height = rectTransform.rect.height - (2 * verticalMargin);
            childHeight = rtChildren[0].rect.height;
            InitializePoolPositions();
        }

        private void InitializePoolPositions()
        {
            float originY = -(height * 0.5f);
            float posOffset = childHeight * 0.5f;
            for (int i = 0; i < rtChildren.Length; i++)
            {
                Vector2 childPos = rtChildren[i].localPosition;
                childPos.y = -4 * childHeight + originY + posOffset + i * (childHeight + itemSpacing);
                rtChildren[i].localPosition = childPos;
            }
        }

        private void SetupPool()
        {
            if (objectsToPoolArray == null)
            {
                return;
            }
            int ind = 0;
            for (int i = 0; i < initPoolSize; i++)
            {
                if (ind > objectsToPoolArray.Length - 1) ind = 0;
                Instantiate(objectsToPoolArray[ind], rectTransform.transform);
                ind= ind + 1;
            }
            rtPool = new RectTransform[rectTransform.childCount];
            for (int i = 0; i < rectTransform.childCount; i++)
            {
                rtPool[i] = rectTransform.GetChild(i) as RectTransform;
            }
        }
    }
}
