using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDialogues : MonoBehaviour
{
    public Text[] textArray;
    public float typingSpeed = 0.05f;

    private int currentIndex = 0;

    public GameObject dialogueBox;

    public static bool inDialogue;

    bool Typing;

    void Start()
    {
        foreach (Text t in textArray)
            t.gameObject.SetActive(false);

        StartCoroutine(Pre1stNight());
    }

    private void Update()
    {
        if(Typing && Input.GetKeyDown(KeyCode.Mouse0))
        {
            typingSpeed = 0.00001f;
        }
    }

    private IEnumerator TypeText(Text textComponent, string fullText)
    {
        textComponent.text = "";

        foreach (char letter in fullText.ToCharArray())
        {
            Typing = true;
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        Typing = false;
    }

    private IEnumerator Pre1stNight()
    {
        inDialogue = true;
        while (currentIndex < textArray.Length)
        {
            Text currentText = textArray[currentIndex];
            currentText.gameObject.SetActive(true);

            yield return StartCoroutine(TypeText(currentText, currentText.text));

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            typingSpeed = 0.03f;

            currentText.gameObject.SetActive(false);
            currentIndex++;

            yield return new WaitForSeconds(0.2f);
        }

        dialogueBox.SetActive(false);
        inDialogue = false;
    }
}
