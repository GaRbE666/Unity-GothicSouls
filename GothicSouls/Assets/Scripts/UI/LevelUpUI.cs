using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class LevelUpUI : MonoBehaviour
    {
        [Header("Player Level")]
        public int currentPlayerLevel; //The current level we are before leveling up
        public int projectedPlayerLevel; //The possible level we will be if we accept leveling up
        public Text currentPlayerlevelText; //The UI text for the number of the current player level
        public Text projectedPlayerLevelText; //The UI text for the projected player number

        [Header("Souls")]
        public Text currentSouls;
        public Text soulsRequiredToLevelUp;

        [Header("Health")]
        public Slider healthSlider;
        public Text currenHealthLevelText;
        public Text projectedHealthLevelText;

        [Header("Stamina")]
        public Slider staminaSlider;
        public Text currentStaminaLevelText;
        public Text projectedStaminaLevelText;

        [Header("Focus")]
        public Slider focusSlider;
        public Text currentFocusLevelText;
        public Text projectedFocusLevelText;

        [Header("Strength")]
        public Slider strengthSlider;
        public Text currentStrengthLevelText;
        public Text projectedStrengthLevelText;

        [Header("Dexterity")]
        public Slider dexteritySlider;
        public Text currentDexterityLevelText;
        public Text projectedDexterityLevelText;

        [Header("Poise")]
        public Slider poiseSlider;
        public Text currentPoiseLevelText;
        public Text projectedPoiseLevelText;

        [Header("Faith")]
        public Slider faithSlider;
        public Text currentFaithLevelText;
        public Text projectedFaithLevelText;

        [Header("Intelligence")]
        public Slider intelligenceSlider;
        public Text currentIntelligenceLevelText;
        public Text projectedIntelligenceLevelText;
    }
}
