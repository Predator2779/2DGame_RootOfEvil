using System;
using Core.Health;
using UnityEngine;

namespace Core.Death
{
    public class DeathEnemy : Base.Death
    {
        [SerializeField] private Health.EnemyHealth enemyHealth;
        private void Start()
        {
            enemyHealth.SetMaxHealth(maxHealth);
        }
        
        public override void Damage(int amount)
        {
            base.Damage(amount);

            DestroyObject(currentHealth);

            enemyHealth.SetCurrentHealth(currentHealth);
        }

        protected void DestroyObject(int health)
        {
            if (health > 0) return;

            Destroy(gameObject);
        }
    }
}