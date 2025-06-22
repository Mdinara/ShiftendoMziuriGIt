using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireGunScript : MonoBehaviour
{
    public static bool FiredUp = false;

    public ParticleSystem FireParticles;

    bool Colliding = false;
    void Start()
    {
        FiredUp = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !FiredUp && Colliding && !EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(FireDuration());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Candle"))
        {
            Colliding = true;
        }
        else
        {
            Colliding = false;
        }
    }

    private IEnumerator FireDuration()
    {
        FiredUp = true;
        FireParticles.Play();
        yield return new WaitForSeconds(20f);
        FiredUp = false;
        FireParticles.Stop();
    }
}
