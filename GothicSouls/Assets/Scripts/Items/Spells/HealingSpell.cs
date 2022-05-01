using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager, bool isLeftHanded)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager, isLeftHanded);
            GameObject instatiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true, false, isLeftHanded);
            Debug.Log("Attempting to cast spell...");
        }

        public override void SucsessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, CameraHandler cameraHandler, PlayerWeaponSlotManager weaponSlotManager, bool isLeftHanded)
        {
            base.SucsessfullyCastSpell(animatorHandler, playerStats, cameraHandler, weaponSlotManager, isLeftHanded);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playerStats.HealPlayer(healAmount);
            Debug.Log("Spell cast successful");

        }
    }
}
