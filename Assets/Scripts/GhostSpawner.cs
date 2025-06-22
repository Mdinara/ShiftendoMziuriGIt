using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject Ghost;

    Animator anim;

    public GameObject CleaningGameObject;

    public int r1 = 1;
    public int r2 = 4;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(SpawnTimer());
        }
    }

    private IEnumerator SpawnTimer()
    {
        if (ManageTime.GhostCount != ManageTime.GhostCountRn && ManageTime.GooCount != ManageTime.GooCountRn)
        {
            r1 = 1;
            r2 = 4;
        }
        
        if(ManageTime.GooCount != ManageTime.GooCountRn && ManageTime.GhostCount >= ManageTime.GhostCountRn)
        {
            r1 = 2;
        }

        if(ManageTime.GooCount >= ManageTime.GooCountRn && ManageTime.GhostCount != ManageTime.GhostCountRn)
        {
            r2 = 2;
        }

        if(ManageTime.GooCount >= ManageTime.GooCountRn && ManageTime.GhostCount >= ManageTime.GhostCountRn)
        {
            r1 = 1;
            r2 = 4;
        }

        int SpawnOrNot = Random.Range(r1, r2);
        if (SpawnOrNot == 1)
        {
            Destroy(CleaningGameObject);
            anim.SetBool("Triggered", true);
            yield return new WaitForSeconds(0.6f);
            Instantiate(Ghost, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
