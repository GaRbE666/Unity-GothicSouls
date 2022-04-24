using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimationManager;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;

        public LayerMask detectionLayer;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider);
        }
    }
}
