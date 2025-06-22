using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDialogueOrder : MonoBehaviour
{
    public GameObject Pre1;
    public GameObject AfterTutorial;
    public GameObject Pre2;
    public GameObject Pre3;
    public GameObject Pre4;
    public GameObject Pre5;
    void Start()
    {
        int Tutorial = PlayerPrefs.GetInt("InTutorial");

        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        if(nightCounter == 0 && Tutorial == 1)
        {
            Pre1.SetActive(true);
        }
        if(nightCounter == 0 && Tutorial == 0)
        {
            AfterTutorial.SetActive(true);
        }
        if(nightCounter == 1)
        {
            Pre2.SetActive(true);
        }
        if (nightCounter == 2)
        {
            Pre3.SetActive(true);
        }
        if (nightCounter == 3)
        {
            Pre4.SetActive(true);
        }
        if (nightCounter == 4)
        {
            Pre5.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
