using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core.Death
{
    public class DeathPlayer : Base.Death
    {

        [SerializeField] private Health.PlayerHealth playerHealth;
        [SerializeField] private string nameScene;
        [SerializeField] private LoadScenes _loadScene;

        private void Start()
        {
            playerHealth.SetMaxHealth(maxHealth);
        }

        public override void Damage(int amount)
        {
            base.Damage(amount);

            DestroyObject(currentHealth);

            playerHealth.SetCurrentHealth(currentHealth);
        }

        protected virtual void DestroyObject(int health)
        {
            if (health > 0) return;

            _loadScene.LoadScenePressed(nameScene);
        }
    }
}