using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    public GameObject PressE;

    public Image ReadSign;

    bool SignOpen = false;

    public GameObject Player;
    void Start()
    {
        
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance <= 1f)
        {
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!SignOpen && Functions.Paused == false)
                {
                    ReadSign.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    SignOpen = true;
                }
                else
                {
                    ReadSign.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                    SignOpen = false;
                }
            }
        }
        else
        {
            PressE.SetActive(false);
        }
    }
}
