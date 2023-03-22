using StrategyGame_2DPlatformer.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class ResourcesPresenter : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("Model")]
        //GameData.instance since I follow a singleton pattern to keep data in one place

        [Header("View")]
        [SerializeField] Text currentMoney;

        private void Start()
        {
            GameData.instance.MoneyChanged += OnMoneyChanged;
        }

        private void OnDestroy()
        {
            GameData.instance.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            if (currentMoney != null) currentMoney.text = GameData.instance.Money.ToString();
        }
    }
}
