using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimationManager;
        EnemyBossManager enemyBossManager;
        public EnemyHealthBar enemyHealthBar;
        public int soulsAwardedOnDeath = 50;

        public bool isBoss;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private void Start()
        {
            if (!isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }
        }

        private float SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth -= damage;

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public override void TakeDamage(int damage, string damageAnimation = "receive_hit")
        {
            base.TakeDamage(damage, damageAnimation = "receive_hit");

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if(isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth,  maxHealth);
            }
            
            enemyAnimationManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimationManager.PlayTargetAnimation("Dead", true);
            isDead = true; 
        }
    }
}
