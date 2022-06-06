using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JS
{
    public class OptionUI : MonoBehaviour
    {
        public PlayerManager player;
        public GameManager gameManager;
        public Text levelText;
        public Text vitalityText;
        public Text staminaText;
        public Text focusText;
        public Text strengthText;
        public Text dexterityText;
        public Text poiseText;
        public Text faithText;
        public Text intelligenceText;
        public Text currentSoulsText;
        public Text currentMaxHpText;
        public Text currentMaxStaminaText;
        public Text timePlayedText;

        private void OnEnable()
        {
            levelText.text = player.playerStatsManager.playerLevel.ToString();
            vitalityText.text = player.playerStatsManager.healthLevel.ToString();
            staminaText.text = player.playerStatsManager.staminaLevel.ToString();
            focusText.text = player.playerStatsManager.focusLevel.ToString();
            strengthText.text = player.playerStatsManager.strenghtLevel.ToString();
            dexterityText.text = player.playerStatsManager.dexterityLevel.ToString();
            poiseText.text = player.playerStatsManager.poiseLevel.ToString();
            faithText.text = player.playerStatsManager.faithLevel.ToString();
            intelligenceText.text = player.playerStatsManager.intelligenceLevel.ToString();
            currentSoulsText.text = player.playerStatsManager.currentSoulCount.ToString();
            currentMaxHpText.text = player.playerStatsManager.maxHealth.ToString();
            currentMaxStaminaText.text = player.playerStatsManager.maxStamina.ToString();
        }

        private void Update()
        {
            timePlayedText.text = gameManager.timeFormated;
        }
    }
}
