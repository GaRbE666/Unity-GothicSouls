using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        #region FIELDS
        EnemyAnimatorManager enemyAnimationManager;
        EnemyBossManager enemyBossManager; 
        public EnemyHealthBar enemyHealthBar;

        public bool isBoss;
        #endregion

        private void Awake()
        {
            enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
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

        public override void TakeDamageNoAnimation(int damage)
        {
            base.TakeDamageNoAnimation(damage);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }
        }

        public void BreakGuard()
        {
            Debug.Log("Rompo guardia");
            enemyAnimationManager.PlayTargetAnimation("Break Guard", true);
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
