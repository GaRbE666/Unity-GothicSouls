using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterAudioManager : MonoBehaviour
    {
        public AudioSource audioSource;

        [Header("FotSteps")]
        public AudioClip walkStepGround;
        public AudioClip runStepGround;
        public AudioClip walkStepWood;
        public AudioClip runStepWood;

        [Header("Criticals")]
        public AudioClip parried;
        public AudioClip criticalHit1;
        public AudioClip criticalHit2;
        public AudioClip backstep;

        [Header("Weapon")]
        public AudioClip spinEffect;

        #region STEPS
        public void PlayWalkStepGround()
        {
            audioSource.PlayOneShot(walkStepGround, 0.3f);
        }

        public void PlayRunStepGround()
        {
            audioSource.PlayOneShot(runStepGround, 0.3f);
        }

        public void PlayWalkStepWood()
        {
            audioSource.PlayOneShot(walkStepWood);
        }

        public void PlayRunStepWood()
        {
            audioSource.PlayOneShot(runStepWood);
        }
        #endregion

        #region CRITICALS
        public void PlayParried()
        {
            audioSource.PlayOneShot(parried);
        }

        public void PlayCriticalHit1()
        {
            audioSource.PlayOneShot(criticalHit1);
        }

        public void PlayciritcalHit2()
        {
            audioSource.PlayOneShot(criticalHit2);
        }

        public void PlayBackstep()
        {
            audioSource.PlayOneShot(backstep);
        }
        #endregion

        #region WEAPON
        public void PlaySpinEffect()
        {
            audioSource.PlayOneShot(spinEffect, .5f);
        }
        #endregion
    }
}
