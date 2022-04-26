using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        #region FIELDS
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimationManager;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;

        public LayerMask detectionLayer;
        #endregion

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider);
        }
    }
}
