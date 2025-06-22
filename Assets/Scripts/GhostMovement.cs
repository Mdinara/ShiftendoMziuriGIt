using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GhostMovement : MonoBehaviour
{
    public GameObject Player;

    Rigidbody2D rb;

    SpriteRenderer sp;

    Animator anim;

    public float Speed;

    public static bool DecreaseHP;

    public ParticleSystem DeathParticles;

    public GameObject Goo;

    public AudioSource GhostSound;

    bool AddedCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        StartCoroutine(SoundsTimer());
    }

    void Update()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance > 1f)
        {
            rb.velocity = direction * Speed;
        }

        if (transform.position.x - Player.transform.position.x < 0)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }

        int Tutorial = PlayerPrefs.GetInt("InTutorial");

        if (distance <= 1f && Tutorial == 0)
        {
            Speed = 0f;
            rb.velocity = Vector2.zero;
            DecreaseHP = true;
        }

        else if (distance >= 1f)
        {
            Speed = 2f;
            DecreaseHP = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AirTrap"))
        {
            StartCoroutine(DestroyTimer());
        }

        if (collision.gameObject.CompareTag("GroundTrap"))
        {
            StartCoroutine(DespawnTimer());
        }
    }

    private IEnumerator DestroyTimer()
    {
        if (!AddedCount)
        {
            AddedCount = true;
            ManageTime.GhostCount++;
        }
        DeathParticles.Play();
        Speed = 0f;
        anim.SetBool("BeingDestroyed", true);
        yield return new WaitForSeconds(0.5f);
        DecreaseHP = false;
        Destroy(gameObject);
    }

    private IEnumerator DespawnTimer()
    {
        Speed = 0.5f;
        anim.SetBool("InTrap", true);
        yield return new WaitForSeconds(0.6f);
        Instantiate(Goo, gameObject.transform.position, gameObject.transform.rotation);
        DecreaseHP = false;
        Destroy(gameObject);
    }

    private IEnumerator SoundsTimer()
    {
        while (true)
        {
            GhostSound.Play();
            yield return new WaitForSeconds(21f);
        }
    }
}
