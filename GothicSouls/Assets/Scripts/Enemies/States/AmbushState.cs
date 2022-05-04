using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectionRadius = 2;
        public string sleepAnimation;
        public string wakeAnimation;

        public LayerMask detectionLayer;

        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemy)
        {
            if (isSleeping && enemy.isInteracting == false)
            {
                enemy.enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);
            }
            #region HANDLE TARGET DETECTION
            Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStatsManager CharacterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                if (CharacterStats != null)
                {
                    Vector3 targetsDirection = CharacterStats.transform.position - enemy.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemy.transform.forward);

                    if (viewableAngle > enemy.minimumDetectionAngle 
                        && viewableAngle < enemy.maximumDetectionAngle)
                    {
                        enemy.currentTarget = CharacterStats;
                        isSleeping = false;
                        enemy.enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }


            #endregion

            #region HANDLE STATE CHANGE
            if (enemy.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
