using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthBar;

        AnimatorHandler animatorHandle;

        private void Awake()
        {
            animatorHandle = GetComponentInChildren<AnimatorHandler>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandle.PlayTargetAnimation("receive_hit", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandle.PlayTargetAnimation("Dead", true);
            }
        }
    }
}
