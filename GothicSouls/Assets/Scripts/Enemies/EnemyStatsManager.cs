using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        #region FIELDS
        EnemyManager enemy;
        public EnemyHealthBar enemyHealthBar;

        public bool isBoss;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<EnemyManager>();
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
            else if (isBoss && enemy.enemyBossManager != null)
            {
                enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }
        }

        public override void TakePoisonDamage(int damage)
        {
            if (enemy.isDead)
            {
                return;
            }

            base.TakePoisonDamage(damage);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemy.enemyBossManager != null)
            {
                enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                enemy.isDead = true;
                enemy.enemyAnimatorManager.PlayTargetAnimation("Dead", true);
            }
        }

        public void BreakGuard()
        {
            enemy.enemyAnimatorManager.PlayTargetAnimation("Break Guard", true);
        }

        public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation)
        {
            base.TakeDamage(physicalDamage, fireDamage, damageAnimation);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if(isBoss && enemy.enemyBossManager != null)
            {
                enemy.enemyBossManager.UpdateBossHealthBar(currentHealth,  maxHealth);
            }

            enemy.enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemy.enemyAnimatorManager.PlayTargetAnimation("Dead", true);
            enemy.isDead = true;
            enemy.enabled = false;
            enemy.backStabCollider = null;
            enemy.riposteCollider = null;
            enemy.characterAudioManager.audioSource.Stop();
            enemy.characterAudioManager.audioSource.enabled = false;
        }
    }
}
