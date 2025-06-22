using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttack : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject Player;

    public float Speed;

    bool StopFollow;

    bool Stop;

    public GameObject AfterExplosion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("Player");
    }


    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance <= 1.5f)
        {
            StopFollow = true;
            if (!Stop) StartCoroutine(StopTimer());
        }

        if (!StopFollow && !Stop)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }
    }

    private IEnumerator StopTimer()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Instantiate(AfterExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Stop = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
