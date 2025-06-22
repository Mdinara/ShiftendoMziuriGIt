using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialObilisk : MonoBehaviour
{
    public GameObject E;

    public GameObject FadeIn;

    public GameObject Player;

    void Start()
    {

    }


    void Update()
    {
        int Tutorial = PlayerPrefs.GetInt("InTutorial");

        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance <= 1.5f)
        {
            E.SetActive(true);
        }
        else
        {
            E.SetActive(false);
        }

        if (distance <= 1.5f && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        PlayerPrefs.SetInt("InTutorial", 0);
        PlayerPrefs.Save();
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

}
