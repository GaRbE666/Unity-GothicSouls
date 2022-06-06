using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JS
{
    public class MenuController : MonoBehaviour
    {
        public Image loadingScreen;
        public GameObject loadingIcon;
        public EventSystem inputsSystem;
        public Animator fadeAnimator;
        public AudioSource audioSource;

        private void Awake()
        {
            fadeAnimator.SetTrigger("fadeOut");
        }

        private void Start()
        {
            if (loadingScreen != null)
            {
                loadingScreen.gameObject.SetActive(false);
            }

            if (loadingIcon != null)
            {
                loadingIcon.SetActive(false);
            }

            if (audioSource != null)
            {
                StartCoroutine(FadeSong(audioSource, 1f, 1));
            }
           
        }

        public void GoToStartNewGame(string sceneName)
        {
            StartCoroutine(Delay(LoadAsynchronously(sceneName)));
        }

        public void GoToMainMenu(string sceneName)
        {
            StartCoroutine(Delay(LoadAsynchronously(sceneName)));
        }

        public void GoToOptionsScene(string sceneName)
        {
            StartCoroutine(Delay(LoadAsynchronously(sceneName)));
        }

        public void GoToExitGame()
        {
            Application.Quit();
        }

        private IEnumerator LoadAsynchronously(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            loadingScreen.gameObject.SetActive(true);
            loadingIcon.SetActive(true);
            while (!operation.isDone)
            {
                yield return null;
            }

            if (audioSource != null)
            {
                StartCoroutine(FadeSong(audioSource, 1f, 0));
            }
            
        }

        private IEnumerator Delay(IEnumerator coroutine)
        {
            fadeAnimator.SetTrigger("fadeIn");
            inputsSystem.enabled = false;
            yield return new WaitForSeconds(2f);
            inputsSystem.enabled = true;
            StartCoroutine(coroutine);
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
