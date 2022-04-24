using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStats : MonoBehaviour
    {
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

            Debug.Log("Total Damage Absorption is " + totalPhysicalDamageAbsorption + "%");

            float finalDamage = physicalDamage; // + fireDamage + magicDamage + lighting Damage + darkDamage;

            currentHealth -= finalDamage;

            Debug.Log("Total Damage Dealt is " + finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }
    }
}
