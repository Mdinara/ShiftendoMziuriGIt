using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackLeft : MonoBehaviour
{
    public GameObject Ghost;
    void Start()
    {
        StartCoroutine(Ghosts());
    }

    void Update()
    {
        
    }

    private IEnumerator Ghosts()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(Ghost, gameObject.transform.position, Quaternion.identity);
        }
    }
}
