using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Storytext : MonoBehaviour
{
    public Text[] textArray;
    public float typingSpeed = 0.05f;

    private int currentIndex = 0;

    void Start()
    {
        foreach (Text t in textArray)
            t.gameObject.SetActive(false);

        StartCoroutine(type());
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

    private IEnumerator type()
    {
        while (currentIndex < textArray.Length)
        {
            Text currentText = textArray[currentIndex];
            currentText.gameObject.SetActive(true);

            yield return StartCoroutine(TypeText(currentText, currentText.text));

            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(FadeOutText(currentText, 1f)); 

            currentText.gameObject.SetActive(false);
            currentIndex++;

            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(Timer1());
    }

    private IEnumerator FadeOutText(Text text, float duration)
    {
        Color originalColor = text.color;
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private IEnumerator Timer1()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}

