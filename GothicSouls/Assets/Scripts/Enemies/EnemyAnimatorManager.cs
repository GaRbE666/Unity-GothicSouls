using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyAnimatorManager : CharacterAnimatorManager
    {
        EnemyManager enemy;

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<EnemyManager>();
        }

        public void AwardSoulsOnDeath()
        {
            PlayerStatsManager playerStats = FindObjectOfType<PlayerStatsManager>();
            SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

            if (playerStats != null)
            {
                playerStats.AddSouls(enemy.enemyStatsManager.soulsAwardedOnDeath);

                if (soulCountBar != null)
                {
                    soulCountBar.SetSoulSountText(playerStats.currentSoulCount);
                }
            }
        }

        public void InstantiateBossParticleFX()
        {
            BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();

            GameObject phaseFX = Instantiate(enemy.enemyBossManager.particleFX, bossFXTransform.transform);
            
        }

        public void PlayWeaponTrailFX()
        {
            enemy.enemyEffectsManager.PlayWeaponFX(false);
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemy.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = enemy.animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemy.enemyRigidBody.velocity = velocity;

            if (enemy.isRotatingWithRootMotion)
            {
                enemy.transform.rotation *= enemy.animator.deltaRotation;
            }
        }
    }
}
