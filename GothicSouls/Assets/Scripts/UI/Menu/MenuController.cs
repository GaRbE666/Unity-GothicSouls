using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SG
{
    public class MenuController : MonoBehaviour
    {
        public Image loadingScreen;
        public GameObject loadingIcon;

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

        }

        public void GoToStartNewGame(string sceneName)
        {
            StartCoroutine(LoadAsynchronously(sceneName));
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
        }

        public void GoToMainMenu(string sceneName)
        {
            StartCoroutine(LoadAsynchronously(sceneName));
        }

        public void GoToOptionsScene(string sceneName)
        {
            StartCoroutine(LoadAsynchronously(sceneName));
        }

        public void GoToExitGame()
        {
            Application.Quit();
        }
    }
}
