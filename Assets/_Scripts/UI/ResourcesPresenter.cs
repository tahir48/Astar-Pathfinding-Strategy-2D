using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer.UI
{
    public class ResourcesPresenter : MonoBehaviour
    {
        //Model
        private GameData Model;
        [Header("View")]
        [SerializeField] Text currentMoney;

        private void Start()
        {
            Model = GameData.instance;
            Model.MoneyChanged += OnMoneyChanged;
        }

        private void OnDestroy()
        {
            Model.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            if (currentMoney != null) currentMoney.text = Model.Money.ToString();
        }
    }
}
