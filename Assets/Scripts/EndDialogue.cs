using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndDialogue : MonoBehaviour
{
    public Text[] textArray;
    public float typingSpeed = 0.05f;

    private int currentIndex = 0;

    public GameObject dialogueBox;

    public GameObject MoneyText;

    public GameObject FadeIn;

    public AudioSource BuySound;

    public GameObject Credits;

    public GameObject Skip;

    public GameObject FadeIn2;

    void Start()
    {
        foreach (Text t in textArray)
            t.gameObject.SetActive(false);

        StartCoroutine(Pre1stNight());
    }

    private IEnumerator TypeText(Text textComponent, string fullText)
    {
        textComponent.text = "";

        foreach (char letter in fullText.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    private IEnumerator Pre1stNight()
    {
        while (currentIndex < textArray.Length)
        {
            Text currentText = textArray[currentIndex];
            currentText.gameObject.SetActive(true);

            yield return StartCoroutine(TypeText(currentText, currentText.text));

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

            if(currentIndex == 3)
            {
                MoneyText.SetActive(true);
                BuySound.Play();
            }

            currentText.gameObject.SetActive(false);
            currentIndex++;

            yield return new WaitForSeconds(0.2f);
        }

        PlayerPrefs.DeleteKey("FirstPlayDone");
        PlayerPrefs.Save();

        FadeIn.SetActive(true);
        dialogueBox.SetActive(false);

        yield return new WaitForSeconds(2f);
        Credits.SetActive(true);

        yield return new WaitForSeconds(5f);
        Skip.SetActive(true);

        yield return new WaitForSeconds(35f);

        FadeIn2.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    public void SkipButton()
    {
        StartCoroutine(SkipCredits());
    }

    public IEnumerator SkipCredits()
    {
        FadeIn2.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
