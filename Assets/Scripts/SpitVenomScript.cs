using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpitVenomScript : MonoBehaviour
{
    Rigidbody2D rb;

    Animator anim;

    public GameObject Player;

    public float Speed;

    bool StopFollow;

    bool Stop;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();    

        Player = GameObject.Find("Player");
    }


    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance <= 1f)
        {
            StopFollow = true;
            if (!Stop) StartCoroutine(StopTimer());
        }

        if (!StopFollow)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            rb.velocity = direction * Speed;
        }
        else
        {
            anim.Play("AfterCollision");
            rb.velocity = transform.right * Speed;
            StartCoroutine(DestroyTimer());
        }
    }

    private IEnumerator StopTimer()
    {
        Speed = 0f;
        yield return new WaitForSeconds(0.15f);
        Speed = 10f;
        Stop = true;
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}

