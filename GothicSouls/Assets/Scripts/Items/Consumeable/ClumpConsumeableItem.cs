using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Items/Consumibles/Cure Effect Clump")]
    public class ClumpConsumeableItem : ConsumibleItem
    {
        [Header("Recovery FX")]
        public GameObject clumpConsumeFX;

        [Header("Cure Fx")]
        public bool curePoison;
        //cure Bleed
        //Cure cursed

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager, PlayerManager player)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager, player);

            if (currentItemAmount > 0)
            {
                currentItemAmount--;
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentItemAmount);
                GameObject clump = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
                playerEffectsManager.currentParticleFX = clumpConsumeFX;
                playerEffectsManager.instantiatedFXModel = clump;

                if (curePoison)
                {
                    playerEffectsManager.poisonBuildup = 0;
                    playerEffectsManager.poisonAmount = playerEffectsManager.defaultPoisonAmount;
                    playerEffectsManager.isPoisoned = false;
                    //Desactivar efecto de envenenado
                }
                weaponSlotManager.rightHandSlot.UnloadWeapon();
            }

        }
    }
}
