using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        #region FIELDS
        EnemyManager enemy;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;

        public LayerMask detectionLayer;
        #endregion

        private void Awake()
        {
            enemy = GetComponent<EnemyManager>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider);
        }

        private void Update()
        {
            enemy.enemyRigidBody.AddForce(Vector3.down * 50);
        }
    }
}
