using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public class EnemyEffectsManager : CharacterEffectsManager
    {
        //BloodPrefabs bloodPrefabs;

        protected override void Awake()
        {
            base.Awake();
            //bloodPrefabs = GetComponent<BloodPrefabs>();
        }

        //public void InstantiateBloodAnim()
        //{
        //    bloodPrefabs.InstantiateBlood(bloodPrefabs.bloodInstancePosition);
        //}
    }
}
