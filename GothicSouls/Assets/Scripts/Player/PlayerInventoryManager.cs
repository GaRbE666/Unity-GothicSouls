using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        #region FIELDS
        PlayerWeaponSlotManager playerWeaponSlotManager;

        [Header("Quick Slot Items")]
        public SpellItem currentSpell;
        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public ConsumibleItem currentConsumable;

        [Header("Current Equipment")]
        public HelmetEquipment currentHelmetEquipment;
        public TorsoEquipment currentTorsoEquipment;
        public HipEquipment currentLegEquipment;
        public ArmsEquipment currentArmEquipment;

        public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[1];
        public WeaponItem[] weaponsInLeftHandSlot = new WeaponItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<WeaponItem> weaponsInventory;
        #endregion

        private void Awake()
        {
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        }

        private void Start()
        {
            rightWeapon = weaponsInRightHandSlot[0];
            leftWeapon = weaponsInLeftHandSlot[0];
            playerWeaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            playerWeaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }

        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex += 1;

            if (currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] == null)
            {
                currentRightWeaponIndex += 1;
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] == null)
            {
                currentRightWeaponIndex += 1;
            }

            if (currentRightWeaponIndex > weaponsInRightHandSlot.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = playerWeaponSlotManager.unarmedWeapon;
                playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmedWeapon, false);
            }
        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex += 1;

            if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlot[0] != null)
            {
                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);
            }
            else if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlot[0] == null)
            {
                currentLeftWeaponIndex += 1;
            }
            else if (currentLeftWeaponIndex == 1 && weaponsInLeftHandSlot[1] != null)
            {
                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);
            }
            else if(currentLeftWeaponIndex == 1 && weaponsInLeftHandSlot[1] == null)
            {
                currentLeftWeaponIndex += 1;
            }

            if (currentLeftWeaponIndex > weaponsInLeftHandSlot.Length -1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = playerWeaponSlotManager.unarmedWeapon;
                playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmedWeapon, true);
            }
        }
    }
}