using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyWeaponSlotManager : CharacterWeaponSlotManager
    {
        #region FIELDS
        public WeaponItem rightHandWeapon;
        public WeaponItem leftHandWeapon;

        EnemyStatsManager enemyStatsManager;
        EnemyEffectsManager enemyEffectsManager;
        #endregion

        private void Awake()
        {
            enemyStatsManager = GetComponent<EnemyStatsManager>();
            enemyEffectsManager = GetComponent<EnemyEffectsManager>();
            LoadWeaponHolderSlots();
        }

        private void Start()
        {
            LoadWeaponsOnBothHands();
        }

        private void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weapon;
                leftHandSlot.LoadWeaponModel(weapon);
                LoadWeaponsDamageCollider(true);
            }
            else
            {
                rightHandSlot.currentWeapon = weapon;
                rightHandSlot.LoadWeaponModel(weapon);
                LoadWeaponsDamageCollider(false);
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            if (rightHandWeapon != null)
            {
                LoadWeaponOnSlot(rightHandWeapon, false);
            }

            if (leftHandSlot != null)
            {
                LoadWeaponOnSlot(leftHandWeapon, true);
            }
        }

        public void LoadWeaponsDamageCollider(bool isLeft)
        {
            if (isLeft)
            {
                leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
                enemyEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
            else
            {
                rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
                enemyEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
        }

        public void OpenDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void DrainStaminaLightAttack()
        {
            
        }

        public void DrainStaminaHeavyAttack()
        {
            
        }

        public void Enablecombo()
        {
            //anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            //anim.SetBool("canDoCombo", false);
        }

        #region HANDLE WEAPON´S POISE BONUS
        public void GrantWeaponAttackingPoiseBonus()
        {
            enemyStatsManager.totalPoiseDefense = enemyStatsManager.totalPoiseDefense + enemyStatsManager.offensivePoiseBonus;
        }

        public void ResetWeaponAttackingPoiseBonus()
        {
            enemyStatsManager.totalPoiseDefense = enemyStatsManager.armorPoiseBonus;
        }
        #endregion
    }
}
