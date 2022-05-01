using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        #region FIELDS
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyBossManager enemyBossManager;
        public EnemyHealthBar enemyHealthBar;

        public bool isBoss;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
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

        public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
        {
            base.TakeDamageNoAnimation(physicalDamage, fireDamage);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }
        }

        public override void TakePoisonDamage(int damage)
        {
            if (isDead)
            {
                return;
            }

            base.TakePoisonDamage(damage);

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
                enemyAnimatorManager.PlayTargetAnimation("Dead", true);
            }
        }

        public void BreakGuard()
        {
            enemyAnimatorManager.PlayTargetAnimation("Break Guard", true);
        }

        public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "receive_hit")
        {
            base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "receive_hit");

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if(isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth,  maxHealth);
            }
            
            enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("Dead", true);
            isDead = true; 
        }
    }
}
