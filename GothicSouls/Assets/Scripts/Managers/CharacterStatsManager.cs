using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStatsManager : MonoBehaviour
    {
        #region FIELDS
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

        //fire Absorption
        //LightingAbsorption
        //Magic Absoprtion
        //Dark Absorption
        public bool isDead;
        #endregion

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }

        private void Start()
        {
            totalPoiseDefense = armorPoiseBonus;
        }

        public virtual void TakeDamage(int physicalDamage, string damageAnimation = "receive_hit")
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

            float finalDamage = physicalDamage; // + fireDamage + magicDamage + lighting Damage + darkDamage;

            currentHealth -= finalDamage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakeDamageNoAnimation(int damage)
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
