using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SG
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        #region FIELDS
        public Animator animator;
        protected CharacterManager characterManager;
        protected CharacterStatsManager characterStatsManager;
        public bool canRotate;

        protected RigBuilder rigBuilder;
        public TwoBoneIKConstraint leftHandConstraint;
        public TwoBoneIKConstraint rightHandConstraint;

        bool handIKWeightsReset = false;
        #endregion

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            rigBuilder = GetComponent<RigBuilder>();
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("canRotate", canRotate);
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnim, 0.2f);
        }

        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isRotatingWithRootMotion", true);
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnim, 0.2f);
        }

        public virtual void CanRotate()
        {
            animator.SetBool("canRotate", true);
        }

        public virtual void StopRotation()
        {
            animator.SetBool("canRotate", false);
        }

        public virtual void Enablecombo()
        {
            animator.SetBool("canDoCombo", true);
        }

        public virtual void DisableCombo()
        {
            animator.SetBool("canDoCombo", false);
        }

        public virtual void EnableIsInvulnerable()
        {
            animator.SetBool("isInvulnerable", true);
        }

        public virtual void DisableIsInvulnerable()
        {
            animator.SetBool("isInvulnerable", false);
        }

        public virtual void EnableIsParrying()
        {
            characterManager.isParrying = true;
        }

        public virtual void DisableIsParrying()
        {
            characterManager.isParrying = false;
        }

        public virtual void EnableCanBeRiposted()
        {
            characterManager.canBeRiposted = true;
        }

        public virtual void DisableCanBeRiposted()
        {
            characterManager.canBeRiposted = false;
        }

        public virtual void TakeCriticalDamageAnimationEvent()
        {
            characterStatsManager.TakeDamageNoAnimation(characterManager.pendingCriticalDamage, 0);
            characterManager.pendingCriticalDamage = 0;
        }

        public virtual void SetHandIKForWeapon(RightHandIKTarget rightHandTarget, LeftHandIKTarget leftHandTarget, bool isTwoHandingWeapon)
        {
            if (isTwoHandingWeapon)
            {
                rightHandConstraint.data.target = rightHandTarget.transform;
                rightHandConstraint.data.targetPositionWeight = 1; //Assign this from a weapon variable if you�d like
                rightHandConstraint.data.targetRotationWeight = 1;

                leftHandConstraint.data.target = leftHandTarget.transform;
                leftHandConstraint.data.targetPositionWeight = 1;
                leftHandConstraint.data.targetRotationWeight = 1;
            }
            else
            {
                rightHandConstraint.data.target = null;
                leftHandConstraint.data.target = null;
            }

            rigBuilder.Build();
        }

        public virtual void CheckHandIKWeight(RightHandIKTarget rightHandIK, LeftHandIKTarget leftHandIK, bool isTwoHandingWeapon)
        {
            if (characterManager.isInteracting)
            {
                return;
            }

            if (handIKWeightsReset)
            {
                handIKWeightsReset = false;

                if (rightHandConstraint.data.target != null)
                {
                    rightHandConstraint.data.target = rightHandIK.transform;
                    rightHandConstraint.data.targetPositionWeight = 1;
                    rightHandConstraint.data.targetRotationWeight = 1;
                }

                if (leftHandConstraint.data.target != null)
                {
                    leftHandConstraint.data.target = leftHandIK.transform;
                    leftHandConstraint.data.targetPositionWeight = 1;
                    leftHandConstraint.data.targetRotationWeight = 1;
                }
            }
        }

        public virtual void EraseHandIKForWeapon()
        {
            handIKWeightsReset = true;

            if (rightHandConstraint.data.target != null)
            {
                rightHandConstraint.data.targetPositionWeight = 0;
                rightHandConstraint.data.targetRotationWeight = 0;
            }

            if (leftHandConstraint.data.target != null)
            {
                leftHandConstraint.data.targetPositionWeight = 0;
                leftHandConstraint.data.targetRotationWeight = 0;
            }
        }
    }
}