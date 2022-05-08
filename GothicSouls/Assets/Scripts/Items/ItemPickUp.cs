using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ItemPickUp : Interactable
    {
        #region FIELDS
        public WeaponItem weapon;
        public HelmetEquipment helmetEquipment;
        public BodyEquipment bodyEquipment;
        public HandEquipment handEquipment;
        public LegEquipment legEquipment;
        public bool isWeapon;
        public bool isHelmet;
        public bool isBody;
        public bool isHand;
        public bool isLeg;
        #endregion

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventoryManager playerInventory;
            PlayerLocomotionManager playerLocomotion;
            PlayerAnimatorManager animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventoryManager>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotionManager>();
            animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stops the player from moving whilst picking up item
            animatorHandler.PlayTargetAnimation("Pick Up Item", true); //Plays the animation of looting the item

            if (isWeapon && weapon != null)
            {
                playerInventory.weaponsInventory.Add(weapon);
                playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
                playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            }
            else if (isHelmet && helmetEquipment != null)
            {
                playerInventory.headEquipmentInventory.Add(helmetEquipment);
                playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = helmetEquipment.itemName;
                playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = helmetEquipment.itemIcon.texture;
            }
            else if (isHand && handEquipment != null)
            {
                playerInventory.handEquipmentInventory.Add(handEquipment);
                playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = handEquipment.itemName;
                playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = handEquipment.itemIcon.texture;
            }
            else if (isLeg && legEquipment != null)
            {
                playerInventory.legEquipmentInventory.Add(legEquipment);
                playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = legEquipment.itemName;
                playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = legEquipment.itemIcon.texture;
            }
            else if (isBody && bodyEquipment != null)
            {
                playerInventory.bodyEquipmentInventory.Add(bodyEquipment);
                playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = bodyEquipment.itemName;
                playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = bodyEquipment.itemIcon.texture;
            }

            playerManager.itemInteractableGameObject.SetActive(true);
            Destroy(gameObject);
        }

        private void PickUpHelmetEquipment(PlayerManager playerManager)
        {
            PlayerInventoryManager playerInventory;
            PlayerLocomotionManager playerLocomotion;
            PlayerAnimatorManager animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventoryManager>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotionManager>();
            animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stops the player from moving whilst picking up item
            animatorHandler.PlayTargetAnimation("Pick Up Item", true); //Plays the animation of looting the item
            playerInventory.headEquipmentInventory.Add(helmetEquipment);
            playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            playerManager.itemInteractableGameObject.SetActive(true);
            Destroy(gameObject);

        }
    }
}
