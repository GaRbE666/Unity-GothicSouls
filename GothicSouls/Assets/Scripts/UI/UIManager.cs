using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject weaponInventoryWindow;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInevntorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;

        private void Start()
        {
            weaponInventorySlots = weaponInevntorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        }

        public void UpdateUI()
        {
            #region WEAPON INVENTORY SLOTS
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInevntorySlotsParent);
                        weaponInventorySlots = weaponInevntorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }

            #endregion
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindows()
        {
            weaponInventoryWindow.SetActive(false);
        }

    }
}
