using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpiderNestScript : MonoBehaviour
{
    SpriteRenderer sp;

    public ParticleSystem OnFlame;

    public ParticleSystem DeathAshes;

    public AudioSource BurnSound;

    bool AddedCount;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private IEnumerator DeathTimer()
    {
        if (!AddedCount)
        {
            AddedCount = true;
            ManageTime.SpiderNestCount++;
        }
        OnFlame.Play();
        BurnSound.Play();
        yield return new WaitForSeconds(0.6f);
        OnFlame.Stop();
        sp.color = Color.black;
        yield return new WaitForSeconds(1f);
        sp.color = new Color(1f, 1f, 1f, 0f);
        DeathAshes.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AirTrap") && FireGunScript.FiredUp == true)
        {
            StartCoroutine(DeathTimer());
        }
    }
}
