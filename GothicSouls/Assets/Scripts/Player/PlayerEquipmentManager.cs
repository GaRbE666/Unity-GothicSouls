using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        #region FIELDS
        InputHandler inputHandler;
        PlayerInventoryManager playerInventory;
        PlayerStatsManager playerStatsManager;

        [Header("Equipment Model Changers")]
        HelmetModelChanger helmetModelChanger;
        TorsoModelChanger torsoModelChanger;
        HipModelChanger hipModelChanger;
        ArmsModelChanger armsModelChanger;

        [Header("Default Naked Models")]
        public GameObject nakedHeadModel;
        public GameObject nakedTorsoModel;
        public GameObject nakedHipModel;
        public GameObject nakedArmsModel;

        public BlockingCollider blockingCollider;
        #endregion

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerInventory = GetComponent<PlayerInventoryManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();

            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipModelChanger>();
            armsModelChanger = GetComponentInChildren<ArmsModelChanger>();
        }

        private void Start()
        {
            EquipAllEquipmentModelsOnStart();
        }

        private void EquipAllEquipmentModelsOnStart()
        {
            //HELMET EQUIPMENT
            helmetModelChanger.UnEquipAllHelmetModels();
            if (playerInventory.currentHelmetEquipment != null)
            {
                nakedHeadModel.SetActive(false);
                helmetModelChanger.EquipHelmetModelByNAme(playerInventory.currentHelmetEquipment.helmetModelName);
                playerStatsManager.physicialDamageAbsorptionHead = playerInventory.currentHelmetEquipment.physicalDefense;
            }
            else
            {
                nakedHeadModel.SetActive(true);
                playerStatsManager.physicialDamageAbsorptionHead = 0;
            }
            
            //TORSO EQUIPMENT
            torsoModelChanger.UnEquipAllTorsoModels();

            if (playerInventory.currentTorsoEquipment != null)
            {
                nakedTorsoModel.SetActive(false);
                torsoModelChanger.EquipTorsoModelByNAme(playerInventory.currentTorsoEquipment.torsoModelName);
                playerStatsManager.physicialDamageAbsorptionBody = playerInventory.currentTorsoEquipment.physicalDefense;
            }
            else
            {
                nakedTorsoModel.SetActive(true);
                playerStatsManager.physicialDamageAbsorptionBody = 0;
            }

            //HIP EQUIPMENT
            hipModelChanger.UnEquipAllHipModels();

            if (playerInventory.currentLegEquipment != null)
            {
                nakedHipModel.SetActive(false);
                hipModelChanger.EquipHipModelByNAme(playerInventory.currentLegEquipment.hipModelName);
                playerStatsManager.physicialDamageAbsorptionLegs = playerInventory.currentLegEquipment.physicalDefense;
            }
            else
            {
                nakedHipModel.SetActive(true);
                playerStatsManager.physicialDamageAbsorptionLegs = 0;
            }

            //ARMS EQUIPMENT
            armsModelChanger.UnEquipAllArmsModels();

            if (playerInventory.currentArmEquipment != null)
            {
                nakedArmsModel.SetActive(false);
                armsModelChanger.EquipArmsModelByNAme(playerInventory.currentArmEquipment.armsModelName);
                playerStatsManager.physicialDamageAbsorptionHands = playerInventory.currentArmEquipment.physicalDefense;
            }
            else
            {
                nakedArmsModel.SetActive(true);
                playerStatsManager.physicialDamageAbsorptionHands = 0;
            }
            
        }

        public void OpenBlockingCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.rightWeapon);
            }
            else
            {
                Debug.Log(playerInventory.leftWeapon);
                blockingCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);
            }
            
            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }
    }
}
