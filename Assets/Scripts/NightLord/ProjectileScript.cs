using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Rigidbody2D rb;

    public float Speed;

    public Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 targetScale = Vector3.one;                 
    public float growSpeed = 2f;                              




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * -Speed;

        transform.localScale = startScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * growSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GhostDestroyer") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
