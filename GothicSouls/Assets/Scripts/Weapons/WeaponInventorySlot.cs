using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        UIManager uiManager;

        public Image icon;
        WeaponItem item;

        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>(); ;
        }

        public void AddItem(WeaponItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (uiManager.rightHandSlot01Selected)
            {
                uiManager.player.playerInventoryManager.weaponsInventory.Add(uiManager.player.playerInventoryManager.weaponsInRightHandSlot[0]);
                uiManager.player.playerInventoryManager.weaponsInRightHandSlot[0] = item;
                uiManager.player.playerInventoryManager.weaponsInventory.Remove(item);
            }
            else if (uiManager.rightHandSlot02Selected)
            {
                uiManager.player.playerInventoryManager.weaponsInventory.Add(uiManager.player.playerInventoryManager.weaponsInRightHandSlot[1]);
                uiManager.player.playerInventoryManager.weaponsInRightHandSlot[1] = item;
                uiManager.player.playerInventoryManager.weaponsInventory.Remove(item);
            }
            else if (uiManager.leftHandSlot01Selected)
            {
                uiManager.player.playerInventoryManager.weaponsInventory.Add(uiManager.player.playerInventoryManager.weaponsInLeftHandSlot[0]);
                uiManager.player.playerInventoryManager.weaponsInLeftHandSlot[0] = item;
                uiManager.player.playerInventoryManager.weaponsInventory.Remove(item);
            }
            else if(uiManager.leftHandSlot02Selected)
            {
                uiManager.player.playerInventoryManager.weaponsInventory.Add(uiManager.player.playerInventoryManager.weaponsInLeftHandSlot[1]);
                uiManager.player.playerInventoryManager.weaponsInLeftHandSlot[1] = item;
                uiManager.player.playerInventoryManager.weaponsInventory.Remove(item);
            }
            else
            {
                return;
            }

            uiManager.player.playerInventoryManager.rightWeapon = uiManager.player.playerInventoryManager.weaponsInRightHandSlot[uiManager.player.playerInventoryManager.currentRightWeaponIndex];
            uiManager.player.playerInventoryManager.leftWeapon = uiManager.player.playerInventoryManager.weaponsInLeftHandSlot[uiManager.player.playerInventoryManager.currentLeftWeaponIndex];

            uiManager.player.playerWeaponSlotManager.LoadWeaponOnSlot(uiManager.player.playerInventoryManager.rightWeapon, false);
            uiManager.player.playerWeaponSlotManager.LoadWeaponOnSlot(uiManager.player.playerInventoryManager.leftWeapon, true);

            uiManager.equipmentWindowUI.LoadWeaponOnEquipmentScreen(uiManager.player.playerInventoryManager);
            uiManager.ResetAllSelectedSlots();
        }
    }
}
