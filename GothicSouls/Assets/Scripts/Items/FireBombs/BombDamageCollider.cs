using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BombDamageCollider : DamageCollider
    {
        #region FIELDS
        [Header("Explosive Damage & Radius")]
        public int explosiveRadius = 1;
        public int explosionDamage;
        public int explosionSplashDamage;
        //MagicExplosionDamage
        //lightningExplosionDamage

        public Rigidbody bombRigidBody;
        private bool hasCollided = false;
        public GameObject impactParticles;
        public AudioSource audioSource;
        public AudioClip explosion;
        #endregion

        protected override void Awake()
        {
            damageCollider = GetComponent<Collider>();
            bombRigidBody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!hasCollided)
            {
                hasCollided = true;
                audioSource.PlayOneShot(explosion);
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity);
                Explode();

                CharacterStatsManager character = collision.transform.GetComponent<CharacterStatsManager>();

                if (character != null)
                {
                    if (character.teamIDNumber != teamIDNumber)
                    {
                        character.TakeDamage(0, explosionDamage, currentDamageAnimation);
                    }  
                }

                Destroy(impactParticles, 5f);
                Destroy(transform.parent.parent.gameObject);
            }
        }

        private void Explode()
        {
            Collider[] characters = Physics.OverlapSphere(transform.position, explosiveRadius);

            foreach (Collider objectsExplosion in characters)
            {
                CharacterStatsManager character = objectsExplosion.GetComponentInParent<CharacterStatsManager>();

                if (character != null)
                {
                    if (character.teamIDNumber != teamIDNumber)
                    {
                        character.TakeDamage(0, explosionSplashDamage, currentDamageAnimation);
                    }
                }
            }
        }
    }
}
