using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class BuildingHealthPresenter : MonoBehaviour
    {
        /// <summary>
        /// This class is responsible for updating the health bar of the building i.e. Presenter.
        /// </summary>
        [SerializeField] private Image healthBar;
        DamageableBuilding damageable;

        private void Start()
        {
            damageable = GetComponent<DamageableBuilding>();
            DamageableBuilding.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            DamageableBuilding.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            healthBar.fillAmount = ((float)damageable.CurrentHealth / (float)damageable.MaxHealth);
        }


    }
}
