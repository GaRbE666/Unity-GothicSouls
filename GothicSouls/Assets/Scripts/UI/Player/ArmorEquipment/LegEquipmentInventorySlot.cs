using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JS
{
    public class LegEquipmentInventorySlot : MonoBehaviour
    {
        UIManager uiManager;
        PlayerManager player;

        public Image icon;
        LegEquipment item;

        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
            player = FindObjectOfType<PlayerManager>();
        }

        public void AddItem(LegEquipment newItem)
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
            if (uiManager.legEquipmentSlotSelected)
            {
                if (uiManager.player.playerInventoryManager.currentLegEquipment != null)
                {
                    player.playerAudioManager.PlayEquipArmor();
                    uiManager.player.playerInventoryManager.legEquipmentInventory.Add(uiManager.player.playerInventoryManager.currentLegEquipment);
                }

                uiManager.player.playerInventoryManager.currentLegEquipment = item;
                uiManager.player.playerInventoryManager.legEquipmentInventory.Remove(item);
                uiManager.player.playerEquipmentManager.EquipAllEquipmentModelsOnStart();
            }
            else
            {
                return;
            }

            uiManager.equipmentWindowUI.LoadArmorOnEquipmentScreen(uiManager.player.playerInventoryManager);
            uiManager.ResetAllSelectedSlots();
        }
    }
}
