using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public GameObject enemy;
        public List<Transform> enemiesSpawns;
        public GameObject enemySleep;
        public List<Transform> enemiesSleepSpawns;
        public Transform enemyParent;

        private void Awake()
        {
            InstantiateAllEnemies();
        }

        public void InstantiateAllEnemies()
        {
            for (int i = 0; i < enemiesSpawns.Count; i++)
            {
                GameObject enemyClone = Instantiate(enemy, enemiesSpawns[i].position, enemiesSpawns[i].rotation);
                enemyClone.transform.SetParent(enemyParent);
            }

            for (int i = 0; i < enemiesSleepSpawns.Count; i++)
            {
                GameObject enemySleepClone = Instantiate(enemySleep, enemiesSleepSpawns[i].position, enemiesSleepSpawns[i].rotation);
                enemySleepClone.transform.SetParent(enemyParent);
            }
        }
    }
}
