using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        PlayerManager player;

        PoisonBuildUpBar poisonBuildUpBar;
        PoisonAmountBar poisonAmountBar;

        public GameObject currentParticleFX; //The particles that will play of the current effect that is effecting the player (drinking estus, poison etc...)
        public GameObject instantiatedFXModel;
        public int amountToBeHealed;
        public int amountToBeFocusUp;
        public bool isHealth;
        public Transform levelUpEffectPosition;
        public GameObject levelUpEffectPrefab;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
            poisonBuildUpBar = FindObjectOfType<PoisonBuildUpBar>();
            poisonAmountBar = FindObjectOfType<PoisonAmountBar>();
        }

        public void DrinkEstusFromEffect()
        {
            if (isHealth)
            {
                player.playerStatsManager.HealPlayer(amountToBeHealed);
            }
            else
            {
                player.playerStatsManager.FocusUpPlayer(amountToBeFocusUp);
            }
            
            GameObject healParticles = Instantiate(currentParticleFX, player.playerStatsManager.transform);
            Destroy(instantiatedFXModel.gameObject);
            player.playerWeaponSlotManager.LoadBothWeaponOnSlots();
            isHealth = false;
        }

        //public void FocusUpPlayerFromEffect()
        //{
        //    //player.playerStatsManager.FocusUpPlayer(amountToBeFocusUp);
        //    GameObject healParticles = Instantiate(currentParticleFX, player.playerStatsManager.transform);
        //    Destroy(instantiatedFXModel.gameObject);
        //    player.playerWeaponSlotManager.LoadBothWeaponOnSlots();
        //}

        protected override void HandlePoisonBuildUp()
        {
            if (poisonBuildup <= 0)
            {
                poisonBuildUpBar.gameObject.SetActive(false);
            }
            else
            {
                poisonBuildUpBar.gameObject.SetActive(true);
            }

            base.HandlePoisonBuildUp();
            poisonBuildUpBar.SetPoisonBuilUpAmount(Mathf.RoundToInt(poisonBuildup));
        }

        protected override void HandlePoisonedEffect()
        {
            if (isPoisoned == false)
            {
                poisonAmountBar.gameObject.SetActive(false);
            }
            else
            {
                poisonAmountBar.gameObject.SetActive(true);
            }
            base.HandlePoisonedEffect();
            poisonAmountBar.SetPoiosnAmount(Mathf.RoundToInt(poisonAmount));
        }
    }
}
