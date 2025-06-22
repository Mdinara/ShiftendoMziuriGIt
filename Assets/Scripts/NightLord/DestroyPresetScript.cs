using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPresetScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
