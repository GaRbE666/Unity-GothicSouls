using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        #region FIELDS
        public PlayerInventoryManager playerInventory;
        public EquipmentWindowUI equipmentWindowUI;
        private QuickSlotsUI quickSlotsUI;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject equipmentScreenWindow;
        public GameObject weaponInventoryWindow;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInevntorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;
        #endregion

        private void Awake()
        {
            quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
        }

        private void Start()
        {
            weaponInventorySlots = weaponInevntorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
            equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventory);
            quickSlotsUI.UpdateCurrentSpellIcon(playerInventory.currentSpell);
            quickSlotsUI.UpdateCurrentConsumableIcon(playerInventory.currentConsumable);
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
            ResetAllSelectedSlots();
            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
        }

    }
}
