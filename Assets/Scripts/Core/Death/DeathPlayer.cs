using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Death
{
    public class DeathPlayer : Base.Death
    {
        [SerializeField] private Health.PlayerHealth playerHealth;
        private void Start()
        {
            playerHealth.SetMaxHealth(maxHealth);
        }

        public override void Damage(int amount)
        {
            base.Damage(amount);
            
            playerHealth.SetCurrentHealth(currentHealth);
        }
    }
}