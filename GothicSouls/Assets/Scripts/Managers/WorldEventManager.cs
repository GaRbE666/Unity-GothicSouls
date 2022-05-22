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
        public GameObject bossDefeatText;
        public AudioSource audioSource;

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
            StartCoroutine(Delay());

            foreach (var fogwall in fogWalls)
            {
                fogwall.ActiveFogWall();
            }

        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFighIsActive = false;
            bossDefeatText.SetActive(true);
            bossDefeatText.GetComponent<Animator>().SetTrigger("defeated");
            StartCoroutine(FadeSong(audioSource, 1f, 0));

            foreach (var fogwall in fogWalls)
            {
                fogwall.DesactivateFogWall();
            }
        }

        private IEnumerator FadeSong(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            yield break;
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(2f);
            audioSource.Play();
            StartCoroutine(FadeSong(audioSource, 1f, 1f));
        }
    }
}
