using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiredScreen : MonoBehaviour
{
    public Text[] textArray;
    public Text[] secondTextArray;
    public float typingSpeed = 0.05f;

    public GameObject dialogueBox;

    public static bool inDialogue;

    public GameObject YesButton;
    public GameObject NoButton;
    void Start()
    {
        foreach (Text t in textArray) t.gameObject.SetActive(false);
        foreach (Text t in secondTextArray) t.gameObject.SetActive(false);

        StartCoroutine(DialogueSequence());
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

    private IEnumerator DialogueSequence()
    {
        inDialogue = true;

        for (int i = 0; i < textArray.Length; i++)
        {
            Text currentText = textArray[i];
            currentText.gameObject.SetActive(true);

            yield return StartCoroutine(TypeText(currentText, currentText.text));

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

            currentText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }

        dialogueBox.SetActive(false);

        for (int i = 0; i < secondTextArray.Length; i++)
        {
            Text currentText = secondTextArray[i];
            currentText.gameObject.SetActive(true);

            yield return StartCoroutine(TypeText(currentText, currentText.text));
            if (i < secondTextArray.Length - 1)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
                currentText.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.2f);
        }

        YesButton.SetActive(true);
        NoButton.SetActive(true);
        inDialogue = false;
    }

    void OnApplicationQuit()
    {
        int Reset = PlayerPrefs.GetInt("FirstPlayDone");
        Reset = 0;
        PlayerPrefs.SetInt("FirstPlayDone", Reset);
        PlayerPrefs.Save();
    }
}
