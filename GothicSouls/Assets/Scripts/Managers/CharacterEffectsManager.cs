using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterManager character;
        public WeaponFX rightWeaponFX;
        public WeaponFX leftWeaponFX;

        [Header("Poison")]
        public GameObject body;
        public bool isPoisoned;
        public float poisonBuildup = 0; //The build up over time that poisons the player after reaching 100
        public float poisonAmount = 100; //The amount of poison the player has to process beofre becoming unpoisoned
        public float defaultPoisonAmount = 100; //The default amount of poison a player has to process once they become posioned
        public float poisonTimer = 2; //The amount of time between each poison damage Tick
        public int poisonDamage = 1;
        float timer;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void PlayWeaponFX(bool isLeft)
        {
            if (isLeft == false)
            {
                if (rightWeaponFX != null)
                {
                    Debug.Log("Ejecuto efecto derecho");
                    rightWeaponFX.PlayWeaponFX();
                }
            }
            else
            {
                if (leftWeaponFX != null)
                {
                    Debug.Log("Ejecuto efecto izquierdo");
                    leftWeaponFX.PlayWeaponFX();
                }
            }
        }

        public virtual void HandleAllBuildUpEffects()
        {

            if (character.isDead)
            {
                return;
            }
            HandlePoisonBuildUp();
            HandlePoisonedEffect();
        }

        protected virtual void HandlePoisonBuildUp()
        {
            if (isPoisoned)
            {
                return;
            }

            if (poisonBuildup > 0 && poisonBuildup < 100)
            {
                poisonBuildup -= 1 * Time.deltaTime;
            }
            else if (poisonBuildup >= 100)
            {
                isPoisoned = true;
                poisonBuildup = 0;

                //Activar efecto de envenenado
            }
        }

        protected virtual void HandlePoisonedEffect()
        {
            if (isPoisoned)
            {
                if (poisonAmount > 0)
                {
                    timer += Time.deltaTime;

                    if (timer >= poisonTimer)
                    {
                        character.characterStatsManager.TakePoisonDamage(poisonDamage);
                        timer = 0;
                    }
                    
                    poisonAmount -= 1 * Time.deltaTime;
                }
                else
                {
                    isPoisoned = false;
                    poisonAmount = defaultPoisonAmount;

                    //desactivar efecto de envenenado
                }
            }
        }
    }
}
