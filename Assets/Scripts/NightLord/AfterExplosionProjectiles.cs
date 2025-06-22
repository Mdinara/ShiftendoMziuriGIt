using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterExplosionProjectiles : MonoBehaviour
{
    Rigidbody2D rb;

    public float Speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * -Speed;

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GhostDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
