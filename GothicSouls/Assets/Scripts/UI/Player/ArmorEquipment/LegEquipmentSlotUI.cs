using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class LegEquipmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;

        public Image icon;
        LegEquipment item;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(LegEquipment hipEquipment)
        {
            if (hipEquipment != null)
            {
                this.item = hipEquipment;
                icon.sprite = this.item.itemIcon;
                icon.enabled = true;
                gameObject.SetActive(true);
            }
            else
            {
                ClearItem();
            }

        }

        public void ClearItem()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void SelectThisSlot()
        {
            uiManager.legEquipmentSlotSelected = true;
        }
    }
}
