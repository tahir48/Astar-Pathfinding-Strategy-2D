using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame_2DPlatformer.Soldiers
{
    public class SoldierHealthPresenter : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        DamageableSoldier damageable;
        // Start is called before the first frame update
        private void Start()
        {
            damageable = GetComponent<DamageableSoldier>();
            DamageableSoldier.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            DamageableSoldier.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            healthBar.fillAmount = ((float)damageable.CurrentHealth / (float)damageable.MaxHealth);
        }

    }
}
