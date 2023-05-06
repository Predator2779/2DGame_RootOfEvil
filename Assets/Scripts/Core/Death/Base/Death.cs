using UnityEngine;

namespace Core.Death.Base
{
    public class Death : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int maxHealth { get; protected set; }

        public int currentHealth { get; protected set; }

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        protected virtual void DestroyObject(int health)
        {
            if (health >= 0) return;

            Destroy(gameObject);
        }

        public virtual void Damage(int amount)
        {
            currentHealth -= amount;

            DestroyObject(currentHealth);
        }
    }
}