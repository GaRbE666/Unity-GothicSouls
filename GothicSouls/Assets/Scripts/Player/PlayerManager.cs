using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        #region FIELDS
        Animator animator;
        CameraHandler cameraHandler;
        public InputHandler inputHandler;
        public PlayerWeaponSlotManager playerWeaponSlotManager;
        public PlayerCombatManager playerCombatManager;
        public PlayerLocomotionManager playerLocomotion;
        public PlayerInventoryManager playerInventoryManager;
        public PlayerStatsManager playerStatsManager;
        public PlayerAnimatorManager playerAnimatorManager;
        public PlayerEffectsManager playerEffectsManager;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            cameraHandler = FindObjectOfType<CameraHandler>();
            backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponent<Animator>();
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerLocomotion = GetComponent<PlayerLocomotionManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
            interactableUI = FindObjectOfType<InteractableUI>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = animator.GetBool("isInteracting");
            canDoCombo = animator.GetBool("canDoCombo");
            isUsingRightHand = animator.GetBool("isUsingRightHand");
            isUsingLeftHand = animator.GetBool("isUsingLeftHand");
            isInvulnerable = animator.GetBool("isInvulnerable");
            isFiringSpell = animator.GetBool("isFiringSpell");
            animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
            animator.SetBool("isInAir", isInAir);
            animator.SetBool("isUnarmed", isUnarmed);
            animator.SetBool("isBlocking", isBlocking);
            //anim.SetBool("isInAir", isInAir); //Jump
            animator.SetBool("isDead", playerStatsManager.isDead);

            inputHandler.TickInput(delta);
            playerAnimatorManager.canRotate = animator.GetBool("canRotate");
            playerLocomotion.HandleRollingAndSprinting(delta);
            //playerLocomotion.HandleJumping(); //Jump
            playerStatsManager.RegenerateStamina();

            CheckForInteractableObject();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRotation(delta);
            playerEffectsManager.HandleAllBuildUpEffects();
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.lt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;
            //inputHandler.jump_Input = false; //Jump
            inputHandler.inventory_Input = false;

            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget();
                cameraHandler.HandleCameraRotation();
            }

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }

        #region PLAYER INTERACTIONS

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.a_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemInteractableGameObject != null && inputHandler.a_Input)
                {
                    itemInteractableGameObject.SetActive(false);
                }
            }
        }

        public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero; // Stop the player from ice skating
            transform.position = playerStandsHereWhenOpeningChest.transform.position;
            playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
        }

        public void PassThroughFogWallInteraction(Transform fogWallEntrance)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero; // Stop the player from ice skating

            Vector3 rotationDirection = fogWallEntrance.transform.forward;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = turnRotation;

            playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);
        }

        #endregion
    }
}
