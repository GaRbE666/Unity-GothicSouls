using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JS
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool useStartPoint;
        [SerializeField] private Transform startPoint;
        [SerializeField] private PlayerManager player;
        public UIManager uiManager;
        public bool isNewGame;
        public float timePlayed;
        public string timeFormated;

        private void Start()
        {
            if (useStartPoint)
            {
                player.transform.position = startPoint.position;
                player.transform.rotation = startPoint.rotation;
                StartingNewGame();
            }
        }

        private void StartingNewGame()
        {
            player.playerAnimatorManager.PlayTargetAnimation("Get Up", true);
        }

        private void Update()
        {
            timePlayed += Time.deltaTime;
            UpdateTimer();
        }

        public void UpdateTimer()
        {

            float hours = Mathf.FloorToInt(timePlayed / 3600) % 24;
            float minutes = Mathf.FloorToInt(timePlayed / 60) % 60;
            float seconds = Mathf.FloorToInt(timePlayed % 60);

            timeFormated = string.Format("{0:00} : {0:00} : {1:00}", minutes, seconds);
        }

        public void ResetLevel()
        {
            SceneManager.LoadScene("Level01");
        }
    }
}
