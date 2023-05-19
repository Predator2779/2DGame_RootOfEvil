using UnityEngine;
using UnityEngine.UI;

namespace Core.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected Image fill;
      
        public virtual void SetCurrentHealth(int health)
        {
            slider.value = health;
        }
    }
}