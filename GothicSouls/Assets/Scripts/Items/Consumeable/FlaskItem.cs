using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Items/Consumibles/Flask")]
    public class FlaskItem : ConsumibleItem
    {
        [Header("Flask Type")]
        public bool estusFlask;
        public bool ashenFlask;

        [Header("Recovery Amount")]
        public int healthRecoveryAmount;
        public int focusPointsRecoveryAmount;

        [Header("Recovery FX")]
        public GameObject recoveryFX;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager, PlayerManager player)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager, player);

            if (currentItemAmount > 0)
            {
                currentItemAmount--;
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentItemAmount);
                GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
                playerEffectsManager.currentParticleFX = recoveryFX;
                playerEffectsManager.amountToBeHealed = healthRecoveryAmount;
                playerEffectsManager.instantiatedFXModel = flask;
                weaponSlotManager.rightHandSlot.UnloadWeapon();
            }
        }
    }
}
