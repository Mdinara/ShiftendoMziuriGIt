using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleDurationScript : MonoBehaviour
{
    public ParticleSystem Fire;

    void Start()
    {
        StartCoroutine(DestroyFire());
    }

    void Update()
    {
        
    }

    private IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(90);
        Fire.Stop();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
