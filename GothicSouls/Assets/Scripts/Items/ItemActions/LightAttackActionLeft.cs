using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    [CreateAssetMenu(menuName = "Item Actions/Light Left Attack Action")]
    public class LightAttackActionLeft : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.playerStatsManager.currentStamina <= 0)
            {
                return;
            }
            player.playerEffectsManager.PlayWeaponFX(true);

            if (player.canDoCombo)
            {
                player.inputHandler.comboFlag = true;
                HandleLightWeaponCombo(player);
                player.inputHandler.comboFlag = false;
            }
            else
            {
                if (player.isInteracting)
                {
                    return;
                }
                if (player.canDoCombo)
                {
                    return;
                }
                HandleLightAttack(player);
            }
        }

        public void HandleLightAttack(PlayerManager player)
        {
            if (player.isUsingLeftHand)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_light_attack_01_Left, true, false);
                player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_01_Left;
                player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.leftWeapon.baseStamina * player.playerInventoryManager.leftWeapon.lightAttackMultiplier);
            }
            else if (player.isUsingRightHand)
            {
                if (player.inputHandler.twoHandFlag)
                {
                    player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.th_light_attack_01, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.th_light_attack_01;
                }
                else
                {
                    player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_light_attack_01_Left, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_01_Left;
                }
                player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.lightAttackMultiplier);
            }
        }

        public void HandleLightWeaponCombo(PlayerManager player)
        {
            if (player.inputHandler.comboFlag)
            {
                player.animator.SetBool("canDoCombo", false);

                if (player.isUsingLeftHand)
                {
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_light_attack_01_Left)
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_light_attack_02_Left, true, false);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_02_Left;
                        player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.leftWeapon.baseStamina * player.playerInventoryManager.leftWeapon.lightAttackMultiplier);
                    }
                    else
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_light_attack_01_Left, true, false);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_01_Left;
                        player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.leftWeapon.baseStamina * player.playerInventoryManager.leftWeapon.lightAttackMultiplier);
                    }
                }
                else if (player.isUsingRightHand)
                {
                    if (player.isTwoHandingWeapon)
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.th_light_attack_01)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.th_light_attack_02, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.th_light_attack_02;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.lightAttackMultiplier);
                        }
                        else
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.th_light_attack_01, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.th_light_attack_01;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.lightAttackMultiplier);
                        }
                    }
                    else
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_light_attack_01_Left)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_light_attack_02_Left, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_02_Left;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.lightAttackMultiplier);
                        }
                        else
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_light_attack_01_Left, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_01_Left;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.lightAttackMultiplier);
                        }
                    }
                }
            }
        }
    }
}
