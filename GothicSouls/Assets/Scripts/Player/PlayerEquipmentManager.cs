using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerStats playerStats;

        [Header("Equipment Model Changers")]
        HelmetModelChanger helmetModelChanger;
        TorsoModelChanger torsoModelChanger;
        HipModelChanger hipModelChanger;
        ArmsModelChanger armsModelChanger;
        //Hand equipment

        [Header("Default Naked Models")]
        public GameObject nakedHeadModel;
        public GameObject nakedTorsoModel;
        public string nakedHipModel;
        public GameObject nakedArmsModel;

        public BlockingCollider blockingCollider;

        private void Awake()
        {
            inputHandler = GetComponentInParent<InputHandler>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerStats = GetComponentInParent<PlayerStats>();
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
                playerStats.physicialDamageAbsorptionHead = playerInventory.currentHelmetEquipment.physicalDefense;
            }
            else
            {
                nakedHeadModel.SetActive(true);
                playerStats.physicialDamageAbsorptionHead = 0;
            }
            
            //TORSO EQUIPMENT
            torsoModelChanger.UnEquipAllTorsoModels();

            if (playerInventory.currentTorsoEquipment != null)
            {
                nakedTorsoModel.SetActive(false);
                torsoModelChanger.EquipTorsoModelByNAme(playerInventory.currentTorsoEquipment.torsoModelName);
                playerStats.physicialDamageAbsorptionBody = playerInventory.currentTorsoEquipment.physicalDefense;
            }
            else
            {
                nakedTorsoModel.SetActive(true);
                playerStats.physicialDamageAbsorptionBody = 0;
            }

            //HIP EQUIPMENT
            hipModelChanger.UnEquipAllHipModels();

            if (playerInventory.currentLegEquipment != null)
            {
                hipModelChanger.EquipHipModelByNAme(playerInventory.currentLegEquipment.hipModelName);
                playerStats.physicialDamageAbsorptionLegs = playerInventory.currentLegEquipment.physicalDefense;
            }
            else
            {
                hipModelChanger.EquipHipModelByNAme(nakedHipModel);
                playerStats.physicialDamageAbsorptionLegs = 0;
            }

            //ARMS EQUIPMENT
            armsModelChanger.UnEquipAllArmsModels();

            if (playerInventory.currentArmEquipment != null)
            {
                nakedArmsModel.SetActive(false);
                armsModelChanger.EquipArmsModelByNAme(playerInventory.currentArmEquipment.armsModelName);
                playerStats.physicialDamageAbsorptionHands = playerInventory.currentArmEquipment.physicalDefense;
            }
            else
            {
                nakedArmsModel.SetActive(true);
                playerStats.physicialDamageAbsorptionHands = 0;
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
