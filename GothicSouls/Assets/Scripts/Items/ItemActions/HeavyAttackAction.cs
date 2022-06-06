using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    [CreateAssetMenu(menuName = "Item Actions/Heavy Attack Action")]
    public class HeavyAttackAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.playerStatsManager.currentStamina <= 0)
            {
                return;
            }
            player.playerEffectsManager.PlayWeaponFX(false);

            if (player.canDoCombo)
            {
                player.inputHandler.comboFlag = true;
                HandleHeavyWeaponCombo(player);
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
                HandleHeavyAttack(player);
            }
        }

        public void HandleHeavyAttack(PlayerManager player)
        {
            if (player.isUsingLeftHand)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true, false);
                player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
                player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.leftWeapon.baseStamina * player.playerInventoryManager.leftWeapon.heavyAttackMultiplier);
            }
            else if (player.isUsingRightHand)
            {
                if (player.inputHandler.twoHandFlag)
                {
                    player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.th_heavy_attack_01, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_01;
                }
                else
                {
                    player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
                }
                player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.heavyAttackMultiplier);
            }
        }

        public void HandleHeavyWeaponCombo(PlayerManager player)
        {
            if (player.inputHandler.comboFlag)
            {
                player.animator.SetBool("canDoCombo", false);

                if (player.isUsingLeftHand)
                {
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_heavy_attack_01)
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_heavy_attack_02, true, false);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_02;
                        player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.leftWeapon.baseStamina * player.playerInventoryManager.leftWeapon.heavyAttackMultiplier);
                    }
                    else
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true, false);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
                        player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.leftWeapon.baseStamina * player.playerInventoryManager.leftWeapon.heavyAttackMultiplier);
                    }
                }
                else if (player.isUsingRightHand)
                {
                    if (player.isTwoHandingWeapon)
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.th_heavy_attack_01)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.th_heavy_attack_02, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_02;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.heavyAttackMultiplier);
                        }
                        else
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.th_heavy_attack_01, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_01;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.heavyAttackMultiplier);
                        }
                    }
                    else
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_heavy_attack_01)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_heavy_attack_02, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_02;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.heavyAttackMultiplier);
                        }
                        else
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_01;
                            player.playerStatsManager.TakeStaminaDamage(player.playerInventoryManager.rightWeapon.baseStamina * player.playerInventoryManager.rightWeapon.heavyAttackMultiplier);
                        }
                    }
                }
            }
        }
    }
}