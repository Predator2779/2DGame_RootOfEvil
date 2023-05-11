using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Core.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

           fill.color = gradient.Evaluate(1f);
        } 

        public void SetCurrentHealth(int health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
    
}
