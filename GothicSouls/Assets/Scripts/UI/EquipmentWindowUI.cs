using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        public HandEquipmentSlotUI[] handEquipmentSlotUI;

        public void LoadWeaponOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for (int i = 0; i < handEquipmentSlotUI.Length; i++)
            {
                if (handEquipmentSlotUI[i].rightHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlot[0]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlot[1]);
                }
                else if (handEquipmentSlotUI[i].leftHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlot[0]);
                }
                else
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlot[1]);
                }
            }
        }

        public void SelectedRightHandSlot01()
        {
            rightHandSlot01Selected = true;
        }
        public void SelectedRightHandSlot02()
        {
            rightHandSlot02Selected = true;
        }

        public void SelectedLeftHandSlot01()
        {
            leftHandSlot01Selected = true;
        }
        public void SelectedLeftHandSlot02()
        {
            leftHandSlot02Selected = true;
        }

    }
}
