using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        Collider damageCollider;
        public bool enabledDamageColliderOnStartUp = false;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseDefence;

        [Header("Damage")]
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledDamageColliderOnStartUp;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStatsManager playerStatsManager = collision.GetComponent<PlayerStatsManager>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                BloodPrefabs bloodPrefabs = collision.GetComponent<BloodPrefabs>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.isParrying)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking)
                    {
                        float physicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsoption) / 100;

                        if (playerStatsManager != null)
                        {
                            playerStatsManager.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                            return;
                        }
                    }
                }

                if (playerStatsManager != null)
                {
                    playerStatsManager.poiseResetTimer = playerStatsManager.totalPoiseResetTime;
                    playerStatsManager.totalPoiseDefense = playerStatsManager.armorPoiseBonus - poiseBreak;
                    Debug.Log("Player´s Poise is currently " + playerStatsManager.totalPoiseDefense);

                    if (playerStatsManager.totalPoiseDefense > poiseBreak)
                    {
                        playerStatsManager.TakeDamageNoAnimation(currentWeaponDamage);                      
                    }
                    else
                    {
                        playerStatsManager.TakeDamage(currentWeaponDamage);
                    }
                    bloodPrefabs.InstantiateBlood(bloodPrefabs.bloodInstancePosition);
                }
            }

            if (collision.tag == "Enemy")
            {
                EnemyStatsManager enemyStatsManager = collision.GetComponent<EnemyStatsManager>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                BloodPrefabs bloodPrefabs = collision.GetComponent<BloodPrefabs>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.isParrying)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking)
                    {
                        float physicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsoption) / 100;

                        if (enemyStatsManager != null)
                        {
                            enemyStatsManager.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                            return;
                        }
                    }
                }

                if (enemyStatsManager != null)
                {
                    enemyStatsManager.poiseResetTimer = enemyStatsManager.totalPoiseResetTime;
                    enemyStatsManager.totalPoiseDefense = enemyStatsManager.armorPoiseBonus - poiseBreak;
                    Debug.Log("Enemies´s Poise is currently " + enemyStatsManager.totalPoiseDefense);

                    if (enemyStatsManager.isBoss)
                    {
                        if (enemyStatsManager.totalPoiseDefense > poiseBreak)
                        {
                            enemyStatsManager.TakeDamageNoAnimation(currentWeaponDamage);
                        }
                        else
                        {
                            enemyStatsManager.TakeDamageNoAnimation(currentWeaponDamage);
                            enemyStatsManager.BreakGuard();
                        }
                    }
                    else
                    {
                        if (enemyStatsManager.totalPoiseDefense > poiseBreak)
                        {
                            enemyStatsManager.TakeDamageNoAnimation(currentWeaponDamage);
                        }
                        else
                        {
                            enemyStatsManager.TakeDamage(currentWeaponDamage);
                        }                       
                    }
                    bloodPrefabs.InstantiateBlood(bloodPrefabs.bloodInstancePosition);
                }
            }

            if (collision.tag == "Illusionary Wall")
            {
                IllusionaryWall illusionaryWall = collision.GetComponent<IllusionaryWall>();
                illusionaryWall.wallHasBeenHit = true;
            }
        }
    }
}
