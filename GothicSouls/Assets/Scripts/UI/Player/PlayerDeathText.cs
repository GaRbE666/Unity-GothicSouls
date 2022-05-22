using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class PlayerDeathText : MonoBehaviour
    {
        public Text deadText;
        public Animator textAnimator;
        public Animator fade;
        public GameManager gameManager;

        private void OnEnable()
        {
            textAnimator.SetTrigger("dead");
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(2f);
            fade.SetTrigger("fadeIn");
            yield return new WaitForSeconds(2f);
            gameManager.ResetLevel();
        }
    }
}
