using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObiliskScript : MonoBehaviour
{
    public GameObject E;

    public GameObject FadeIn;

    public GameObject Player;

    public static bool EndNightEarly;

    void Start()
    {
        EndNightEarly = false;
    }


    void Update()
    {
        int passed = PlayerPrefs.GetInt("Passed");

        float distance = Vector2.Distance(transform.position, Player.transform.position);

        int nightcounter = PlayerPrefs.GetInt("NightCounter");

        if (distance <= 1.5f)
        {
            E.SetActive(true);
        }
        else
        {
            E.SetActive(false);
        }

        if (distance <= 1.5f && Input.GetKeyDown(KeyCode.E) && passed == 1)
        {
            StartCoroutine(Timer());
        }

        if (distance <= 1.5f && Input.GetKeyDown(KeyCode.E) && passed == 1 && nightcounter == 5)
        {
            StartCoroutine(Timer3());
        }

        if (distance <= 1.5f && Input.GetKeyDown(KeyCode.E) && passed == 0)
        {
            StartCoroutine(Timer2());
        }
    }

    private IEnumerator Timer()
    {
        EndNightEarly = true;
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    private IEnumerator Timer2()
    {
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    private IEnumerator Timer3()
    {
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(7);
    }
}
