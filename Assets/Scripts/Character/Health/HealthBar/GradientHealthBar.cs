using UnityEngine;

namespace Core.Health
{
    public class GradientHealthBar : HealthBar
    {
        [SerializeField] private Gradient gradient;

        public override void SetCurrentHealth(int health)
        {
            base.SetCurrentHealth(health);

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}