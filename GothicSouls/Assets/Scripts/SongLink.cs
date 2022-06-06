using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public class SongLink : MonoBehaviour
    {
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 11)
            {
                audioSource.Play();
                StartCoroutine(FadeSong(audioSource, 1f, 1));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 11)
            {
                StartCoroutine(FadeSong(audioSource, 2f, 0)); 
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
    }
}
