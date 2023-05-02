using UnityEngine;

namespace Death.Base
{
    public class Death : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth { get; set; }

        public int currentHealth { get; protected set; }

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void Damage(int amount)
        {
            currentHealth -= amount;
        }
    }
}