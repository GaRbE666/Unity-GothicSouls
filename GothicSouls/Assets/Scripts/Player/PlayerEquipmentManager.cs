using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        #region FIELDS
        PlayerManager player;

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
            player = GetComponent<PlayerManager>();

            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipModelChanger>();
            armsModelChanger = GetComponentInChildren<ArmsModelChanger>();
        }

        private void Start()
        {
            EquipAllEquipmentModelsOnStart();
        }

        public void EquipAllEquipmentModelsOnStart()
        {
            //HELMET EQUIPMENT
            helmetModelChanger.UnEquipAllHelmetModels();
            if (player.playerInventoryManager.currentHelmetEquipment != null)
            {
                nakedHeadModel.SetActive(false);
                helmetModelChanger.EquipHelmetModelByNAme(player.playerInventoryManager.currentHelmetEquipment.helmetModelName);
                player.playerStatsManager.physicialDamageAbsorptionHead = player.playerInventoryManager.currentHelmetEquipment.physicalDefense;
            }
            else
            {
                nakedHeadModel.SetActive(true);
                player.playerStatsManager.physicialDamageAbsorptionHead = 0;
            }
            
            //TORSO EQUIPMENT
            torsoModelChanger.UnEquipAllTorsoModels();

            if (player.playerInventoryManager.currentTorsoEquipment != null)
            {
                nakedTorsoModel.SetActive(false);
                torsoModelChanger.EquipTorsoModelByNAme(player.playerInventoryManager.currentTorsoEquipment.torsoModelName);
                player.playerStatsManager.physicialDamageAbsorptionBody = player.playerInventoryManager.currentTorsoEquipment.physicalDefense;
            }
            else
            {
                nakedTorsoModel.SetActive(true);
                player.playerStatsManager.physicialDamageAbsorptionBody = 0;
            }

            //HIP EQUIPMENT
            hipModelChanger.UnEquipAllHipModels();

            if (player.playerInventoryManager.currentLegEquipment != null)
            {
                nakedHipModel.SetActive(false);
                hipModelChanger.EquipHipModelByNAme(player.playerInventoryManager.currentLegEquipment.hipModelName);
                player.playerStatsManager.physicialDamageAbsorptionLegs = player.playerInventoryManager.currentLegEquipment.physicalDefense;
            }
            else
            {
                nakedHipModel.SetActive(true);
                player.playerStatsManager.physicialDamageAbsorptionLegs = 0;
            }

            //ARMS EQUIPMENT
            armsModelChanger.UnEquipAllArmsModels();

            if (player.playerInventoryManager.currentArmEquipment != null)
            {
                nakedArmsModel.SetActive(false);
                armsModelChanger.EquipArmsModelByNAme(player.playerInventoryManager.currentArmEquipment.armsModelName);
                player.playerStatsManager.physicialDamageAbsorptionHands = player.playerInventoryManager.currentArmEquipment.physicalDefense;
            }
            else
            {
                nakedArmsModel.SetActive(true);
                player.playerStatsManager.physicialDamageAbsorptionHands = 0;
            }
            
        }

        public void OpenBlockingCollider()
        {
            if (player.inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(player.playerInventoryManager.rightWeapon);
            }
            else
            {
                Debug.Log(player.playerInventoryManager.leftWeapon);
                blockingCollider.SetColliderDamageAbsorption(player.playerInventoryManager.leftWeapon);
            }
            
            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }
    }
}
