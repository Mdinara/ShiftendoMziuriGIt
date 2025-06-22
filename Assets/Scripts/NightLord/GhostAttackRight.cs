using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackRight : MonoBehaviour
{
    public GameObject Ghost;
    void Start()
    {

    }

    void Update()
    {

    }

    private IEnumerator Ghosts()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(Ghost, gameObject.transform.position, Quaternion.identity);
        }
    }
}
