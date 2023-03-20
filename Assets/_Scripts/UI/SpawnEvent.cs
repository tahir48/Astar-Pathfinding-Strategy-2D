using System;
using UnityEngine;

namespace StrategyGame_2DPlatformer.UI
{
    public class SpawnEvent : MonoBehaviour
    {
        public static event Action<string> onSpawnButtonClick;
        public void HandleButtonClick(string soldierName)
        {
            // This method will be called when the button is clicked.

            onSpawnButtonClick.Invoke(soldierName);
        }

    }
}
