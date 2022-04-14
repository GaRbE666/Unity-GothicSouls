using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;

        public WeaponItem unarmedWeapon;

        public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[1];
        public WeaponItem[] weaponsInLeftHandSlot = new WeaponItem[1];

        public int currentRightWeaponIndex;
        public int currentLeftWeaponIndex;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            currentRightWeaponIndex = -1;
            currentLeftWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            leftWeapon = unarmedWeapon;
        }

        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex += 1;

            if (currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] == null)
            {
                currentRightWeaponIndex += 1;
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] == null)
            {
                currentRightWeaponIndex += 1;
            }

            if (currentRightWeaponIndex > weaponsInRightHandSlot.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex += 1;

            if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlot[0] != null)
            {
                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);
            }
            else if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlot[0] == null)
            {
                currentLeftWeaponIndex += 1;
            }
            else if (currentLeftWeaponIndex == 1 && weaponsInLeftHandSlot[1] != null)
            {
                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);
            }
            else if(currentLeftWeaponIndex == 1 && weaponsInLeftHandSlot[1] == null)
            {
                currentLeftWeaponIndex += 1;
            }

            if (currentLeftWeaponIndex > weaponsInLeftHandSlot.Length -1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
            }
        }
    }
}