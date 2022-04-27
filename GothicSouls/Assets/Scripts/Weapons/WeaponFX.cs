using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public MeleeWeaponTrail trail;
        public float timeToStopTrail;
        //FireWeapon trail
        //Dark weapon trail
        //Lighting weapon trail

        public void PlayWeaponFX()
        {
            StopAllCoroutines();
            trail.Emit = true;
            StartCoroutine(StopWeaponFX());
        }

        private IEnumerator StopWeaponFX()
        {
            yield return new WaitForSeconds(timeToStopTrail);
            trail.Emit = false;
        }
    }
}
