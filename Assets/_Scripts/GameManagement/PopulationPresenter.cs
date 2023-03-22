using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class PopulationPresenter : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("Model")]
        //GameData.instance since I follow a singleton pattern to keep data in one place

        [Header("View")]
        [SerializeField] Text currentPop;
        [SerializeField] Text availaiblePop;

        private void Start()
        {
            GameData.instance.PopulationChanged += OnPopulationChanged;
        }

        private void OnDestroy()
        {
            GameData.instance.PopulationChanged -= OnPopulationChanged;
        }

        private void OnPopulationChanged()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            Debug.LogError("Pop has changed");
            if (currentPop != null) currentPop.text = GameData.instance.CurrentHumanPopulationSize.ToString();
            if (availaiblePop != null) availaiblePop.text = "/ " + GameData.instance.CurrentPopulationSize.ToString();
        }



    }
}
