using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStatsManager : MonoBehaviour
    {
        #region FIELDS
        CharacterAnimatorManager characterAnimatorManager;

        [Header("Team I.D")]
        public int teamIDNumber = 0;

        public int healthLevel = 10;
        public float maxHealth;
        public float currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public int focusLevel = 10;
        public float maxFocusPoints;
        public float currentFocusPoints;

        public int soulCount = 0;
        public int soulsAwardedOnDeath = 50;

        [Header("Poise")]
        public float totalPoiseDefense; // The total poise during damage calculation
        public float offensivePoiseBonus; // The poise you Gain during an attack with a weapon
        public float armorPoiseBonus; //The posie you Gain from wearing what ever you have equipped
        public float totalPoiseResetTime = 15;
        public float poiseResetTimer = 0;

        [Header("Armor Absorptions")]
        public float physicialDamageAbsorptionHead;
        public float physicialDamageAbsorptionBody;
        public float physicialDamageAbsorptionLegs;
        public float physicialDamageAbsorptionHands;

        public float fireDamageAbsorptionHead;
        public float fireDamageAbsorptionBody;
        public float fireDamageAbsorptionLegs;
        public float fireDamageAbsorptionHands;

        //LightingAbsorption
        //Magic Absoprtion
        //Dark Absorption
        public bool isDead;
        #endregion

        protected virtual void Awake()
        {
            characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        }

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }

        private void Start()
        {
            totalPoiseDefense = armorPoiseBonus;
        }

        public virtual void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "receive_hit")
        {
            if (isDead)
            {
                return;
            }

            characterAnimatorManager.EraseHandIKForWeapon();

            float totalPhysicalDamageAbsorption = 1 -
                (1 - physicialDamageAbsorptionHead / 100) *
                (1 - physicialDamageAbsorptionBody / 100) *
                (1 - physicialDamageAbsorptionLegs / 100) *
                (1 - physicialDamageAbsorptionHands / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            float totalFireDamageAbsorption = 1 -
                (1 - fireDamageAbsorptionHead / 100) *
                (1 - fireDamageAbsorptionBody / 100) *
                (1 - fireDamageAbsorptionLegs / 100) *
                (1 - fireDamageAbsorptionHands / 100);

            fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

            float finalDamage = physicalDamage + fireDamage; // + magicDamage + lighting Damage + darkDamage;

            currentHealth -= Mathf.RoundToInt(currentHealth - finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
        {
            if (isDead)
            {
                return;
            }

            float totalPhysicalDamageAbsorption = 1 -
                (1 - physicialDamageAbsorptionHead / 100) *
                (1 - physicialDamageAbsorptionBody / 100) *
                (1 - physicialDamageAbsorptionLegs / 100) *
                (1 - physicialDamageAbsorptionHands / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            float totalFireDamageAbsorption = 1 -
                (1 - fireDamageAbsorptionHead / 100) *
                (1 - fireDamageAbsorptionBody / 100) *
                (1 - fireDamageAbsorptionLegs / 100) *
                (1 - fireDamageAbsorptionHands / 100);

            fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

            float finalDamage = physicalDamage + fireDamage; // + magicDamage + lighting Damage + darkDamage;

            currentHealth -= Mathf.RoundToInt(currentHealth - finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakePoisonDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }
    }
}
