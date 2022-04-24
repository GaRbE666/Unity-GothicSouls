using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WorldEventManager : MonoBehaviour
    {
        public List<FogWall> fogWalls;
        public UIBossHealthBar bossHealthBar;
        public EnemyBossManager boss;

        public bool bossFighIsActive; //Is currently fighting boss
        public bool bossHasBeenAwakened; //Wake the boss/watched cutscene but died during fight
        public bool bossHasBeenDefeated; //Boss has been defeated

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        public void ActivateBossFight()
        {
            bossFighIsActive = true;
            bossHasBeenAwakened = true;
            bossHealthBar.SetUIHealthBarToActive();

            foreach (var fogwall in fogWalls)
            {
                fogwall.ActiveFogWall();
            }

        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFighIsActive = false;

            foreach (var fogwall in fogWalls)
            {
                fogwall.DesactivateFogWall();
            }
        }
    }
}
