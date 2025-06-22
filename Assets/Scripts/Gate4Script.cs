using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate4Script : MonoBehaviour
{
    public GameObject E;

    public GameObject Closed;

    public GameObject Open;

    bool Colliding = false;

    void Start()
    {
        Colliding = false;
        OpenGate();
    }

    void Update()
    {
        if (Colliding == true && Input.GetKeyDown(KeyCode.E))
        {
            int OpenGate = PlayerPrefs.GetInt("OpenGate4");
            OpenGate = 1;
            PlayerPrefs.SetInt("OpenGate4", 1);
            PlayerPrefs.Save();

            Closed.SetActive(false);
            Open.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            E.SetActive(true);
            Colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            E.SetActive(false);
            Colliding = false;
        }
    }

    void OpenGate()
    {
        int OpenGate = PlayerPrefs.GetInt("OpenGate4");

        if (OpenGate == 1)
        {
            Closed.SetActive(false);
            Open.SetActive(true);
        }
        else
        {
            Closed.SetActive(true);
            Open.SetActive(false);
        }
    }
}
