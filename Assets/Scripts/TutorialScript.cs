using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text WASDText;

    public Text Time;
    public Text Bars;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "RemoveWASD")
        {
            WASDText.enabled = false;
        }

        if(collision.gameObject.tag == "Tut2")
        {
            Time.gameObject.SetActive(true);
            Bars.gameObject.SetActive(true);
        }

        if(collision.gameObject.name == "RemoveTut2")
        {
            Time.gameObject.SetActive(false);
            Bars.gameObject.SetActive(false); 
        }
    }
}
