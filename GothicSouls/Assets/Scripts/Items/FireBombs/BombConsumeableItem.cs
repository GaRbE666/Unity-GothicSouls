using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    [CreateAssetMenu (menuName ="Items/Consumeables/Bomb Item")]
    public class BombConsumeableItem : ConsumibleItem
    {
        [Header("Velocity")]
        public int upwardVelocity = 50;
        public int forwardVelocity = 50;
        public int bombMass = 1;

        [Header("Live Bomb Model")]
        public GameObject liveBombModel;

        [Header("Base Damage")]
        public int baseDamage = 200;
        public int explosiveDamage = 75;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager, PlayerManager player)
        {
            if (currentItemAmount > 0)
            {
                currentItemAmount--;
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentItemAmount);
                weaponSlotManager.rightHandSlot.UnloadWeapon();
                playerAnimatorManager.PlayTargetAnimation(consumeAnimation, true);
                GameObject bombModel = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, weaponSlotManager.rightHandSlot.transform);
                playerEffectsManager.instantiatedFXModel = bombModel;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("No", true);
            }
        }
    }
}
