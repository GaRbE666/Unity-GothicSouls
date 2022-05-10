using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        #region FIELDS
        public List<WeaponItem> weaponsInventory;
        public List<SpellItem> spellInventory;
        public List<ConsumibleItem> consumablesInventory;
        public List<HelmetEquipment> headEquipmentInventory;
        public List<BodyEquipment> bodyEquipmentInventory;
        public List<LegEquipment> legEquipmentInventory;
        public List<HandEquipment> handEquipmentInventory;
        public PlayerManager player;
        #endregion

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            foreach (ConsumibleItem item in consumablesInventory)
            {
                item.currentItemAmount = item.maxItemAmount;
            }
        }

        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex += 1;

            if (currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] != null)
            {
                
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] == null)
            {
                currentRightWeaponIndex += 1;
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] == null)
            {
                currentRightWeaponIndex += 1;
            }

            if (currentRightWeaponIndex > weaponsInRightHandSlot.Length - 1)
            {
                currentRightWeaponIndex = 0;
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex += 1;

            if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlot[0] != null)
            {

                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);

                if (leftWeapon.weaponType == WeaponType.FaithCaster)
                {
                    player.playerInventoryManager.currentSpell = spellInventory[0];
                    player.uiManager.quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
                }
                else if (leftWeapon.weaponType == WeaponType.PyromancyCaster)
                {
                    player.playerInventoryManager.currentSpell = spellInventory[1];
                    player.uiManager.quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
                }

            }
            else if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlot[0] == null)
            {
                currentLeftWeaponIndex += 1;
            }
            else if (currentLeftWeaponIndex == 1 && weaponsInLeftHandSlot[1] != null)
            {

                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);

                if (leftWeapon.weaponType == WeaponType.FaithCaster)
                {
                    player.playerInventoryManager.currentSpell = spellInventory[0];
                    player.uiManager.quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
                }
                else if (leftWeapon.weaponType == WeaponType.PyromancyCaster)
                {
                    player.playerInventoryManager.currentSpell = spellInventory[1];
                    player.uiManager.quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
                }
            }
            else if(currentLeftWeaponIndex == 1 && weaponsInLeftHandSlot[1] == null)
            {
                currentLeftWeaponIndex += 1;
            }

            if (currentLeftWeaponIndex > weaponsInLeftHandSlot.Length -1)
            {

                currentLeftWeaponIndex = 0;
                leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlot[currentLeftWeaponIndex], true);
                if (leftWeapon.weaponType == WeaponType.FaithCaster)
                {
                    player.playerInventoryManager.currentSpell = spellInventory[0];
                    player.uiManager.quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
                }
                else if (leftWeapon.weaponType == WeaponType.PyromancyCaster)
                {
                    player.playerInventoryManager.currentSpell = spellInventory[1];
                    player.uiManager.quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
                }
            }
        }

        public void ChangeConsumableItem() 
        {
            currentConsumableIndex += 1;

            if (currentConsumableIndex == 0 && consumablesInventory[0] != null)
            {
                currentConsumable = consumablesInventory[currentConsumableIndex];
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableIcon(currentConsumable);
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentConsumable.currentItemAmount);
            }
            else if (currentConsumableIndex == 0 && consumablesInventory[0] == null)
            {
                currentConsumableIndex += 1;
            }
            else if (currentConsumableIndex == 1 && consumablesInventory[1] != null)
            {
                currentConsumable = consumablesInventory[currentConsumableIndex];
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableIcon(currentConsumable);
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentConsumable.currentItemAmount);
            }
            else if (currentConsumableIndex == 1 && consumablesInventory[1] == null)
            {
                currentConsumableIndex += 1;
            }
            else if (currentConsumableIndex == 2 && consumablesInventory[2] != null)
            {
                currentConsumable = consumablesInventory[currentConsumableIndex];
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableIcon(currentConsumable);
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentConsumable.currentItemAmount);
            }
            else if (currentConsumableIndex == 2 && consumablesInventory[2] == null)
            {
                currentConsumableIndex += 1;
            }
            else if (currentConsumableIndex == 3 && consumablesInventory[3] != null)
            {
                currentConsumable = consumablesInventory[currentConsumableIndex];
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableIcon(currentConsumable);
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentConsumable.currentItemAmount);
            }
            else if (currentConsumableIndex == 3 && consumablesInventory[3] == null)
            {
                currentConsumableIndex += 1;
            }

            if (currentConsumableIndex > consumablesInventory.Count - 1)
            {
                currentConsumableIndex = 0;
                currentConsumable = consumablesInventory[currentConsumableIndex];
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableIcon(currentConsumable);
                player.uiManager.quickSlotsUI.UpdateCurrentConsumableText(currentConsumable.currentItemAmount);

            }
        }
    }
}