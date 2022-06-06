using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public class DamageCollider : MonoBehaviour
    {
        #region FIELDS
        public CharacterManager characterManager;
        protected Collider damageCollider;
        public bool enabledDamageColliderOnStartUp = false;

        [Header("Team I.D")]
        public int teamIDNumber = 0;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseDefence;

        [Header("Damage")]
        public int physicalDamage;
        public int fireDamage;
        public int magicDamage;
        public int lightningDamage;
        public int dakrDamage;

        protected bool shieldHasBeenHit;
        protected bool hasBeenParried;
        protected string currentDamageAnimation;
        #endregion

        protected virtual void Awake()
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

        protected virtual void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Character"))
            {
                shieldHasBeenHit = false;
                hasBeenParried = false;

                CharacterStatsManager characterStats = collision.GetComponent<CharacterStatsManager>();
                CharacterManager characterManager = collision.GetComponent<CharacterManager>();
                BloodPrefabs bloodPrefabs = collision.GetComponent<BloodPrefabs>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (characterManager.isDead)
                {
                    return;
                }

                if (characterManager != null)
                {
                    if (characterStats.teamIDNumber == teamIDNumber)
                    {
                        return;
                    }

                    CheckForParry(characterManager);

                    CheckForBlock(characterManager, characterStats, shield);
                }

                if (characterStats != null)
                {
                    if (characterStats.teamIDNumber == teamIDNumber)
                    {
                        return;
                    }

                    if (hasBeenParried)
                    {
                        return;
                    }

                    if (shieldHasBeenHit)
                    {
                        return;
                    }

                    characterStats.poiseResetTimer = characterStats.totalPoiseResetTime;
                    characterStats.totalPoiseDefense = characterStats.totalPoiseDefense - poiseBreak;
                    float directionHitFrom = (Vector3.SignedAngle(characterManager.transform.forward, characterManager.transform.forward, Vector3.up));
                    ChooseWichDirectionDamageCameFrom(directionHitFrom);

                    if (characterStats.totalPoiseDefense > poiseBreak)
                    {
                        characterStats.TakeDamageNoAnimation(physicalDamage, 0);                      
                    }
                    else
                    {
                        characterStats.TakeDamage(physicalDamage, 0, currentDamageAnimation);
                    }
                    characterManager.characterAudioManager.PlayRandomHit();
                    bloodPrefabs.InstantiateBlood(bloodPrefabs.bloodInstancePosition);
                }
            }

            if (collision.CompareTag("Illusionary Wall"))
            {
                IllusionaryWall illusionaryWall = collision.GetComponent<IllusionaryWall>();
                illusionaryWall.wallHasBeenHit = true;
            }
        }

        protected virtual void CheckForParry(CharacterManager enemyManager)
        {
            if (enemyManager.isParrying)
            {
                characterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Parried", true);
                hasBeenParried = true;
            }
        }

        protected virtual void CheckForBlock(CharacterManager enemyManager, CharacterStatsManager enemyStats, BlockingCollider shield)
        {
            if (shield != null && enemyManager.isBlocking)
            {
                float physicalDamageAfterBlock = physicalDamage - (physicalDamage * shield.blockingPhysicalDamageAbsoption) / 100;
                float fireDamageAfterBlock = fireDamage - (fireDamage * shield.blockingFireDamageAbsorption) / 100;

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), 0, "Block Guard");
                    shieldHasBeenHit = true;
                }
            }

        }

        protected virtual void ChooseWichDirectionDamageCameFrom(float direction)
        {
            Debug.Log(direction);
            if (direction >= 145 && direction <= 180)
            {
                Debug.Log("Forward");
                currentDamageAnimation = "Damage_Forward_01";
            }
            else if (direction <= -145 && direction >= -180)
            {
                Debug.Log("Forward");
                currentDamageAnimation = "Damage_Forward_01";
            }
            else if (direction >= -45 && direction <= 45)
            {
                Debug.Log("Back");
                currentDamageAnimation = "Damage_Back_01";
            }
            else if (direction >= -144 && direction <= -45)
            {
                Debug.Log("Left");
                currentDamageAnimation = "Damage_Left_01";
            }
            else if (direction >= 45 && direction <= 144)
            {
                Debug.Log("Right");
                currentDamageAnimation = "Damage_Right_01";
            }
        }
    }
}
