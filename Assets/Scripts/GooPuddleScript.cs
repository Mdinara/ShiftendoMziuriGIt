using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GooPuddleScript: MonoBehaviour
{
    Animator anim;

    public GameObject Spawner;

    private bool Colliding;

    public static int Money;

    bool AddedMoney;

    bool AddedCount;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Colliding == true && GooCleanerScript.TrapChosen == false && !EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(DestroyTimer());
            if (!AddedMoney)
            {
                Money += 1;
                AddedMoney = true;
                StartCoroutine(Timer());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Gun"))
        {
            Colliding = true;
        }
        else
        {
            Colliding = false;
        }
    }

    private IEnumerator DestroyTimer()
    {
        if (!AddedCount)
        {
            AddedCount = true;
            ManageTime.GooCount++;
        }
        anim.SetBool("IsCleaned", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Destroy(Spawner);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        AddedMoney = false;
    }
}
