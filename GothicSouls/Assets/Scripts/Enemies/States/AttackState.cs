using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AttackState : State
    {
        public RotateTowardsTargetState rotateTowardsTargetState;
        public CombatStanceState combatStanceState;
        public PursueTargetState pursueTargetState;
        public EnemyAttackAction currentAttack;

        bool willDoComboOnNextAttack = false;
        public bool hasPerformedAttack = false;

        public override State Tick(EnemyManager enemy)
        {
            float distanceFromTarget = Vector3.Distance(enemy.currentTarget.transform.position, enemy.transform.position);
            RotateTowardTargetWhileAttacking(enemy);

            if (distanceFromTarget > enemy.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if (willDoComboOnNextAttack && enemy.canDoCombo)
            {
                AttackTargetWithCombo(enemy);
            }

            if (!hasPerformedAttack)
            {
                AttackTarget(enemy);
                RollForComboChance(enemy);
            }

            if (willDoComboOnNextAttack && hasPerformedAttack)
            {
                return this;
            }

            return rotateTowardsTargetState;
        }

        private void AttackTarget(EnemyManager enemy)
        {
            enemy.animator.SetBool("isUsingRightHand", currentAttack.isRightHandedAction);
            enemy.animator.SetBool("isUsingLeftHand", !currentAttack.isRightHandedAction);
            enemy.UpdateWhichHandCharacterIsUsing(currentAttack.comboAction.isRightHandedAction);
            enemy.enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemy.enemyAnimatorManager.PlayWeaponTrailFX();
            enemy.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyManager enemy)
        {
            enemy.animator.SetBool("isUsingRightHand", currentAttack.isRightHandedAction);
            enemy.animator.SetBool("isUsingLeftHand", !currentAttack.isRightHandedAction);
            willDoComboOnNextAttack = false;
            enemy.enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemy.enemyAnimatorManager.PlayWeaponTrailFX();
            enemy.currentRecoveryTime = currentAttack.recoveryTime;
            currentAttack = null;
        }

        private void RotateTowardTargetWhileAttacking(EnemyManager enemyManager)
        {
            //Rotate manually
            if (enemyManager.canRotate && enemyManager.isInteracting)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }

        private void RollForComboChance(EnemyManager enemyManager)
        {
            float comboChance = Random.Range(0, 100);

            if (enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
            {
                if (currentAttack.comboAction != null)
                {
                    willDoComboOnNextAttack = true;
                    currentAttack = currentAttack.comboAction;
                }
                else
                {
                    willDoComboOnNextAttack = false;
                    currentAttack = null;
                }
            }
        }
    }
}
