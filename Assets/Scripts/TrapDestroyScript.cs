using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDestroyScript : MonoBehaviour
{
    int GhostCount;

    public AudioSource DestroyAud;

    bool HasPlayed = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (GhostCount == 3)
        {
            StartCoroutine(Timer());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ghost"))
        {
            GhostCount ++;
        }
    }

    private IEnumerator Timer()
    {
        if (!HasPlayed)
        {
            DestroyAud.Play();
            HasPlayed = true;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
