using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject FadeIn;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Play()
    {
        if (!PlayerPrefs.HasKey("FirstPlayDone"))
        {
            StartCoroutine(FirstPlayReset());
        }
        else
        {
            StartCoroutine(Timer1());
        }
    }

    private IEnumerator FirstPlayReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        yield return new WaitForEndOfFrame();
        
        PlayerPrefs.SetInt("FirstPlayDone", 1);
        PlayerPrefs.SetInt("NightCounter", 0);
        PlayerPrefs.SetInt("Passed", 0);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("InTutorial", 1);
        PlayerPrefs.SetInt("OpenGate4", 0);

        PlayerPrefs.Save();

        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(5);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator Timer1()
    {
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
