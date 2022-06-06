using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager, bool isLeftHanded)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager, isLeftHanded);
            GameObject instatiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            if (isLeftHanded)
            {
                animatorHandler.PlayTargetAnimation(spellLeftAnimation, true, false, isLeftHanded);
            }
            else
            {
                animatorHandler.PlayTargetAnimation(spellAnimation, true, false, isLeftHanded);
            }
        }

        public override void SucsessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, CameraHandler cameraHandler, PlayerWeaponSlotManager weaponSlotManager, bool isLeftHanded)
        {
            base.SucsessfullyCastSpell(animatorHandler, playerStats, cameraHandler, weaponSlotManager, isLeftHanded);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playerStats.HealPlayer(healAmount);
        }
    }
}
