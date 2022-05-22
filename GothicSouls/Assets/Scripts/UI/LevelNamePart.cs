using System.Collections;
using UnityEngine;
using TMPro;

public class LevelNamePart : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public string nameLevelText;
    public Animator animatorText;
    public AudioSource audioSource;

    private bool hasEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasEntered)
        {
            hasEntered = true;
            levelText.text = nameLevelText;
            StartCoroutine(LevelTextNameCoroutine());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasEntered)
        {
            hasEntered = true;
            levelText.text = nameLevelText;
            StartCoroutine(LevelTextNameCoroutine());
        }
    }

    private IEnumerator LevelTextNameCoroutine()
    {
        animatorText.SetBool("textAppear", true);
        audioSource.Play();
        yield return new WaitForSeconds(3f);
        animatorText.SetBool("textAppear", false);
        yield return new WaitForSeconds(2f);
        hasEntered = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
