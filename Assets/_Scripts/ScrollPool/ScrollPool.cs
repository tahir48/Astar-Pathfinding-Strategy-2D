using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class ScrollPool : MonoBehaviour
    {
        #region Pool Related Variables
        [SerializeField] private uint initPoolSize;
        public uint InitPoolSize => initPoolSize;
        [SerializeField] private PooledObject[] objectToPoolArray;
        #endregion


        #region Scroll Related Variables
        public float ChildHeight { get { return heightOfPooledobject; } }
        
        private RectTransform rectTransform;
        private RectTransform[] rtPool;
        private float  heightOfPooledobject;

        [SerializeField] private float verticalMargin;
        #endregion

        private void Awake()
        {
            SetupPool();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {

            #region Pool Related Assignments
            List<RectTransform> activePoolElements = new List<RectTransform>();
            for (int i = 0; i < rectTransform.childCount; i++)
            {
                RectTransform child = rectTransform.GetChild(i) as RectTransform;
                if (child.gameObject.activeSelf)
                {
                    activePoolElements.Add(child);
                }
            }

            rtPool = new RectTransform[activePoolElements.Count];
            for (int i = 0; i < activePoolElements.Count; i++)
            {
                rtPool[i] = activePoolElements[i];
            }
            #endregion

            #region Scroll Related Assignments
            heightOfPooledobject = rtPool[0].rect.height;

            ArrangePoolPositionsAtStart();
            #endregion
        }
        private void SetupPool()
        {
            if (objectToPoolArray == null)
            {
                return;
            }

            PooledObject instance = null;

            int index = 0;
            for (int i = 0; i < initPoolSize; i++)
            {
                if (index > objectToPoolArray.Length - 1) index = 0;
                instance = Instantiate(objectToPoolArray[index], gameObject.transform);
                instance.Pool = this;
                index = index + 1;
            }
        }
        [SerializeField] int _margin;
        //Set the vertical position of each RectTransform in an array of RectTransforms
        //relative to the parent RectTransform
        private void ArrangePoolPositionsAtStart()
        {
            float center = (rectTransform.rect.height * 0.5f);
            for (int i = 0; i < rtPool.Length; i++)
            {
                Vector2 childPos = rtPool[i].localPosition;
                childPos.y = center + i * (heightOfPooledobject + _margin);
                rtPool[i].localPosition = childPos;
            }
        }
    }
}