using UnityEngine;
using UnityEngine.UI;

namespace Core.Health
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image fill;
        
        // Start is called before the first frame update

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetCurrentHealth(int health)
        {
            slider.value = health;
        }
    }
}
