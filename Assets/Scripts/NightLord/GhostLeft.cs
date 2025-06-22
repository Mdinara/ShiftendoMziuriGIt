using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLeft : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        rb.velocity = new Vector2(4, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GhostDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
