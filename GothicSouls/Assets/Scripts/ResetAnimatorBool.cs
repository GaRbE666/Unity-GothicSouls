using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public string isUsingRightHand = "isUsingRightHand";
        public bool isUsingRightHandStatus = false;

        public string isUsingLeftHand = "isUsingLeftHand";
        public bool isUsingLeftHandStatus = false;

        public string isInvulnerable = "isInvulnerable";
        public bool isInvulnerableStatus = false;

        public string isInteractingBool = "isInteracting";
        public bool isInteractingStatus = false;

        public string isFiringSpellBool = "isFiringSpell";
        public bool isFiringSpellStatus = false;

        public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
        public bool isrotatingWithRootMotionStatus = false;

        public string canRotate = "canRotate";
        public bool canRotateStatus = true;

        public string isMirroredBool = "isMirrored";
        public bool isMirroredStatus = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager character = animator.GetComponent<CharacterManager>();

            character.isUsingLeftHand = false;
            character.isUsingRightHand = false;

            animator.SetBool(isInteractingBool, isInteractingStatus);
            animator.SetBool(isFiringSpellBool, isFiringSpellStatus);
            animator.SetBool(isRotatingWithRootMotion, isrotatingWithRootMotionStatus);
            animator.SetBool(canRotate, canRotateStatus);
            animator.SetBool(isInvulnerable, isInvulnerableStatus);
            animator.SetBool(isMirroredBool, isMirroredStatus);
        }
    }
}
