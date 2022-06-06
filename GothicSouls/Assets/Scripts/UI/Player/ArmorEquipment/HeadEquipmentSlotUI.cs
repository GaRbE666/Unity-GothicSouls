using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JS
{
    public class HeadEquipmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;

        public Image icon;
        HelmetEquipment item;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(HelmetEquipment helmetEquipment)
        {
            if (helmetEquipment != null)
            {
                this.item = helmetEquipment;
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
            uiManager.headEquipmentSlotSelected = true;
        }
    }
}
