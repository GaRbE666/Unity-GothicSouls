using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BlockingCollider : MonoBehaviour
    {
        public BoxCollider blockingCollider;

        public float blockingPhysicalDamageAbsoption;

        private void Awake()
        {
            blockingCollider = GetComponent<BoxCollider>();
        }

        public void SetColliderDamageAbsorption(WeaponItem weapon)
        {
            if (weapon != null)
            {
                blockingPhysicalDamageAbsoption = weapon.physicalDamageAbsorption;
            }
        }

        public void EnableBlockingCollider()
        {
            blockingCollider.enabled = true;
        }

        public void DisableBlockingCollider()
        {
            blockingCollider.enabled = false;
        }
    }
}
