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

        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);
            }
            #region HANDLE TARGET DETECTION
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStatsManager CharacterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                if (CharacterStats != null)
                {
                    Vector3 targetsDirection = CharacterStats.transform.position - enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle 
                        && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = CharacterStats;
                        isSleeping = false;
                        enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }


            #endregion

            #region HANDLE STATE CHANGE
            if (enemyManager.currentTarget != null)
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
