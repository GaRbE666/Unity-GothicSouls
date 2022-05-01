using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerCombatManager : MonoBehaviour
    {
        #region FIELDS
        InputHandler inputHandler;
        CameraHandler cameraHandler;
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerEquipmentManager playerEquipmentManager;
        PlayerStatsManager playerStatsManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerWeaponSlotManager playerWeaponSlotManager;
        PlayerEffectsManager playerEffectsManager;

        [Header("Attack Animations")]
        public string oh_light_attack_01 = "OH_Light_Attack_01";
        public string oh_light_attack_02 = "OH_Light_Attack_02";
        public string oh_heavy_attack_01 = "OH_Heavy_Attack_01";

        public string th_light_attack_01 = "TH_Light_Attack_01";
        public string th_light_attack_02 = "TH_Light_Attack_02";
        public string th_heavy_attack_01 = "TH_Heavy_Attack_01";

        public string weapon_art = "Weapon_Art";

        public string lastAttack;

        LayerMask riposteLayer = 1 << 13;
        LayerMask backStabLayer = 1 << 12;
        #endregion

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerManager = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
            inputHandler = GetComponent<InputHandler>();

        }

        public void HandleHoldRBAction()
        {
            if (playerManager.isTwoHandingWeapon)
            {
                PerformRBRangedAction();
            }
            else
            {

            }
        }

        public void HandleRBAction()
        {
            playerAnimatorManager.EraseHandIKForWeapon();

            if (playerInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword ||playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
            {
                PerformRBMeleeAction();
            }
            else if (playerInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster || playerInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster || playerInventoryManager.rightWeapon.weaponType == WeaponType.PyromancyCaster)
            {
                PerformMagicAction(playerInventoryManager.rightWeapon, true);
            }
        }

        public void HandleRTAction()
        {
            playerAnimatorManager.EraseHandIKForWeapon();

            if (playerInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword || playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
            {
                PerformRTMeleeAction();
            }
            else if (playerInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster || playerInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster || playerInventoryManager.rightWeapon.weaponType == WeaponType.PyromancyCaster)
            {
                PerformMagicAction(playerInventoryManager.rightWeapon, true);
            }
        }

        public void HandleLTAction()
        {
            if (playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield || playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
            {
                PerformLTWeaponArt(inputHandler.twoHandFlag);
            }
            else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSword)
            {

            }
        }

        public void HandleLBAction()
        {
            if (playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield || playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSword)
            {
                PerformLBBlockingAction();
            }
            else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.FaithCaster || playerInventoryManager.leftWeapon.weaponType == WeaponType.PyromancyCaster)
            {
                PerformMagicAction(playerInventoryManager.leftWeapon, true);
                playerAnimatorManager.animator.SetBool("isUsingLeftHand", true);
            }
        }

        private void HandleLightWeaponCombo(WeaponItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
            {
                return;
            }

            if (inputHandler.comboFlag)
            {
                playerAnimatorManager.animator.SetBool("CanDoCombo", false);

                if (lastAttack == oh_light_attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(oh_light_attack_02, true);
                }
                else if (lastAttack == th_light_attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(th_light_attack_02, true);
                }
            }
        }

        private void HandleHeavyWeaponCombo(WeaponItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
            {
                return;
            }

            if (inputHandler.comboFlag)
            {
                playerAnimatorManager.animator.SetBool("CanDoCombo", false);

                if (lastAttack == oh_heavy_attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(oh_heavy_attack_01, true);
                }
                else if (lastAttack == th_heavy_attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(th_heavy_attack_01, true);
                }
            }
        }

        private void HandleHeavyAttack(WeaponItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
            {
                return;
            }

            playerWeaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {
                playerAnimatorManager.PlayTargetAnimation(th_light_attack_01, true);
                lastAttack = th_heavy_attack_01;
            }
            else
            {

                playerAnimatorManager.PlayTargetAnimation(oh_heavy_attack_01, true);
                lastAttack = oh_light_attack_01;
            }
        }

        private void PerformRBMeleeAction()
        {
            playerAnimatorManager.animator.SetBool("isUsingRightHand", true);

            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleLightWeaponCombo(playerInventoryManager.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                {
                    return;
                }
                if (playerManager.canDoCombo)
                {
                    return;
                }

                playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
                //HandleLightAttack(playerInventoryManager.rightWeapon);
            }
            playerEffectsManager.PlayWeaponFX(false);
        }

        private void PerformRTMeleeAction()
        {
            playerAnimatorManager.animator.SetBool("isUsingRightHand", true);

            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleHeavyWeaponCombo(playerInventoryManager.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                {
                    return;
                }
                if (playerManager.canDoCombo)
                {
                    return;
                }

                //playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
                HandleHeavyAttack(playerInventoryManager.rightWeapon);
            }
            playerEffectsManager.PlayWeaponFX(false);
        }

        private void PerformRBRangedAction()
        {
            if (playerStatsManager.currentStamina <= 0)
            {
                return;
            }

            playerAnimatorManager.EraseHandIKForWeapon();
            playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
            
        }

        private void PerformLBBlockingAction()
        {
            if (playerManager.isInteracting)
            {
                return;
            }

            if (playerManager.isBlocking)
            {
                return;
            }

            playerAnimatorManager.PlayTargetAnimation("Block Start", false, true);
            playerEquipmentManager.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }

        private void PerformMagicAction(WeaponItem weapon, bool isLeftHanded)
        {
            if (playerManager.isInteracting)
            {
                return;
            }

            if (weapon.weaponType == WeaponType.FaithCaster)
            {
                if (playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isFaithSpell)
                {
                    if (playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost)
                    {
                        playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager, isLeftHanded);
                    }
                    else
                    {
                        playerAnimatorManager.PlayTargetAnimation("No", true);
                    }
                }
            }
            else if (weapon.weaponType == WeaponType.PyromancyCaster)
            {
                if (playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isPyroSpell)
                {
                    if (playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost)
                    {
                        playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager, isLeftHanded);
                    }
                    else
                    {
                        playerAnimatorManager.PlayTargetAnimation("No", true);
                    }
                }
            }
        }

        private void PerformLTWeaponArt(bool isTwoHanding)
        {
            if (playerManager.isInteracting)
            {
                return;
            }

            if (isTwoHanding)
            {

            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(weapon_art, true);
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventoryManager.currentSpell.SucsessfullyCastSpell(playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager, playerManager.isUsingLeftHand);
            playerAnimatorManager.animator.SetBool("isFiringSpell", true);
        }

        public void AttemptBackStabOrRiposte()
        {
            if (playerStatsManager.currentStamina <= 0)
            {
                return;
            }

            RaycastHit hit;

            if (Physics.Raycast(inputHandler.critialAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null)
                {
                    //CHECK FOR TEAM I.D (so you can back stab fiends or yourserf?)
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPoint.position;

                    Vector3 rotationDirecton = playerManager.transform.root.eulerAngles;
                    rotationDirecton = hit.transform.position - playerManager.transform.position;
                    rotationDirecton.y = 0;
                    rotationDirecton.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirecton);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    playerAnimatorManager.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                    //do damage
                }
            }
            else if (Physics.Raycast(inputHandler.critialAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
                {
                    playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPoint.position;

                    Vector3 rotationDirection = playerManager.transform.rotation.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Riposted", true);
                }
            }
        }
    }
}
