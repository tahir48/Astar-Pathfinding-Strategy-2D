using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer.UI
{
    public class PopulationPresenter : MonoBehaviour
    {
        //Model
        private GameData Model;
        [Header("View")]
        [SerializeField] Text currentPop;
        [SerializeField] Text availaiblePop;

        private void Start()
        {
            Model = GameData.instance;
            Model.PopulationChanged += OnPopulationChanged;
        }

        private void OnDestroy()
        {
            Model.PopulationChanged -= OnPopulationChanged;
        }

        private void OnPopulationChanged()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            if (currentPop != null) currentPop.text = Model.CurrentPopulation.ToString();
            if (availaiblePop != null) availaiblePop.text = "/ " + Model.AvailaiblePopulation.ToString();
        }
    }
}
