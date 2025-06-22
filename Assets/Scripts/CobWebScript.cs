using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobWebScript : MonoBehaviour
{
    Rigidbody2D rb;

    Animator anim;

    public GameObject Player;

    public float Speed;

    public static bool Parried;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player");

        Vector2 direction = (Player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.velocity = direction * Speed;

        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("AirTrap"))
        {
            rb.velocity = -rb.velocity;
            Parried = true;
        }

        if(Parried == true && collision.gameObject.CompareTag("Spider"))
        {
            Destroy(gameObject);
        }
    }
}
