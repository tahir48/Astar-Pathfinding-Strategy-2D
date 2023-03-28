using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer
{
    public class BuildingHealthPresenter : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private Image healthBar;
        DamageableBuilding damageable;
        // Start is called before the first frame update
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
