using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;

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
            }
            else
            {
                nakedHeadModel.SetActive(true);
            }
            
            //TORSO EQUIPMENT
            torsoModelChanger.UnEquipAllTorsoModels();

            if (playerInventory.currentTorsoEquipment != null)
            {
                nakedTorsoModel.SetActive(false);
                torsoModelChanger.EquipTorsoModelByNAme(playerInventory.currentTorsoEquipment.torsoModelName);
            }
            else
            {
                nakedTorsoModel.SetActive(true);
            }

            //HIP EQUIPMENT
            hipModelChanger.UnEquipAllHipModels();

            if (playerInventory.currentLegEquipment != null)
            {
                hipModelChanger.EquipHipModelByNAme(playerInventory.currentLegEquipment.hipModelName);
            }
            else
            {
                hipModelChanger.EquipHipModelByNAme(nakedHipModel);
            }

            //ARMS EQUIPMENT
            armsModelChanger.UnEquipAllArmsModels();

            if (playerInventory.currentArmEquipment != null)
            {
                nakedArmsModel.SetActive(false);
                armsModelChanger.EquipArmsModelByNAme(playerInventory.currentArmEquipment.armsModelName);
            }
            else
            {
                nakedArmsModel.SetActive(true);
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
