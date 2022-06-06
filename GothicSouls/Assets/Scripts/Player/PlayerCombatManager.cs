using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public class PlayerCombatManager : MonoBehaviour
    {
        #region FIELDS
        PlayerManager player;

        [Header("Attack Animations")]
        public string oh_light_attack_01 = "OH_Light_Attack_01";
        public string oh_light_attack_02 = "OH_Light_Attack_02";
        public string oh_heavy_attack_01 = "OH_Heavy_Attack_01";
        public string oh_heavy_attack_02 = "OH_Heavy_Attack_02";

        public string oh_light_attack_01_Left = "OH_Light_Attack_01_Left";
        public string oh_light_attack_02_Left = "OH_Light_Attack_02_Left";
        public string oh_heavy_attack_01_Left = "OH_Heavy_Attack_01_Left";
        public string oh_heavy_attack_02_Left = "OH_Heavy_Attack_02_Left";


        public string th_light_attack_01 = "TH_Light_Attack_01";
        public string th_light_attack_02 = "TH_Light_Attack_02";
        public string th_heavy_attack_01 = "TH_Heavy_Attack_01";
        public string th_heavy_attack_02 = "TH_Heavy_Attack_02";

        public string weapon_art = "Weapon_Art";

        public string lastAttack;

        LayerMask riposteLayer = 1 << 13;
        LayerMask backStabLayer = 1 << 12;
        #endregion

        private void Awake()
        {
            player = GetComponent<PlayerManager>();

        }

        private void SuccessfullyCastSpell()
        {
            player.playerInventoryManager.currentSpell.SucsessfullyCastSpell(player.playerAnimatorManager, player.playerStatsManager, player.cameraHandler, player.playerWeaponSlotManager, player.isUsingLeftHand);
            player.animator.SetBool("isFiringSpell", true);
        }

        public void AttemptBackStabOrRiposte()
        {
            if (player.playerStatsManager.currentStamina <= 0)
            {
                return;
            }

            RaycastHit hit;

            if (Physics.Raycast(player.inputHandler.critialAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = player.playerWeaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null)
                {
                    //CHECK FOR TEAM I.D (so you can back stab fiends or yourserf?)
                    player.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPoint.position;

                    Vector3 rotationDirecton = player.transform.root.eulerAngles;
                    rotationDirecton = hit.transform.position - player.transform.position;
                    rotationDirecton.y = 0;
                    rotationDirecton.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirecton);
                    Quaternion targetRotation = Quaternion.Slerp(player.transform.rotation, tr, 500 * Time.deltaTime);
                    player.transform.rotation = targetRotation;

                    int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    player.playerAnimatorManager.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                }
            }
            else if (Physics.Raycast(player.inputHandler.critialAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = player.playerWeaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
                {
                    player.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPoint.position;

                    Vector3 rotationDirection = player.transform.rotation.eulerAngles;
                    rotationDirection = hit.transform.position - player.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(player.transform.rotation, tr, 500 * Time.deltaTime);
                    player.transform.rotation = targetRotation;

                    int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    player.playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Riposted", true);
                }
            }
        }
    }
}
