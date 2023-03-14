using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace StrategyGame_2DPlatformer
{
    public class SpawnEvent : MonoBehaviour
    {
        public static event Action onSpawnButtonClick;

        public void HandleButtonClick()
        {
            // This method will be called when the button is clicked.
            onSpawnButtonClick.Invoke();
        }

    }
}
