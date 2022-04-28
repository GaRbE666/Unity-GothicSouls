using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PoisonSurface : MonoBehaviour
    {
        public float poisonBuildUpAmoun = 7;

        public List<CharacterEffectsManager> charactesInsidePoisonSurface;

        private void OnTriggerEnter(Collider other)
        {
            CharacterEffectsManager character = other.GetComponent<CharacterEffectsManager>();

            if (character != null)
            {
                charactesInsidePoisonSurface.Add(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CharacterEffectsManager character = other.GetComponent<CharacterEffectsManager>();

            if (character != null)
            {
                charactesInsidePoisonSurface.Remove(character);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            foreach (CharacterEffectsManager character in charactesInsidePoisonSurface)
            {
                if (character.isPoisoned)
                {
                    return;
                }
                Debug.Log("Hago buildup");
                character.poisonBuildup = character.poisonBuildup + poisonBuildUpAmoun * Time.deltaTime;
            }
        }
    }
}
