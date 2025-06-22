using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    Animator anim;

    public GameObject Player;

    public float Speed = 1.5f;

    public GameObject Spit;
    public GameObject SpitSpawnPoint;

    bool AttackCooldown;
    bool StopAttacking;

    public AudioSource SpitSound;
    public AudioSource CobwebSound;

    public int AttackCounter;

    public GameObject Cobweb;
    public GameObject CobwebSpawnPoint;

    public ParticleSystem SpiderDeathParticles;
    public AudioSource SpiderDeathAudio;
    public AudioSource SpiderPreDeathAudio;

    public CircleCollider2D FollowTrigger;

    bool CanFollow;
    bool AddedCount;

    public float bobSpeed = 1f; 
    public float bobHeight = 0.1f; 

    private float originalY;

    public float acceleration = 5f;
    public float deceleration = 5f;
    Vector2 currentVelocity;

    public ParticleSystem OnFlame;
    public AudioSource BurnSound;

    public ParticleSystem DeathAshes;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        originalY = transform.position.y;
    }

    void Update()
    {
        if (CanFollow) FollowPlayer();

        if (!CanFollow && rb.velocity == Vector2.zero)
        {
            float newY = originalY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private IEnumerator SpitAttack()
    {
        AttackCounter++;
        AttackCooldown = true;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.6f);
        SpitSound.Play();
        Instantiate(Spit, SpitSpawnPoint.transform.position, Spit.transform.rotation);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Attacking", false);

        if (AttackCounter >= 3)
        {
            yield return StartCoroutine(CobwebAttack());
            AttackCounter = 0;
        }

        yield return new WaitForSeconds(3f);
        AttackCooldown = false;
    }

    private IEnumerator CobwebAttack()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Cobwebing", true);

        yield return new WaitForSeconds(0.7f);
        CobwebSound.Play();
        Instantiate(Cobweb, CobwebSpawnPoint.transform.position, Cobweb.transform.rotation);

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Cobwebing", false);

        yield return new WaitForSeconds(3f);
    }

    private IEnumerator DeathTimer()
    {
        if (!AddedCount)
        {
            AddedCount = true;
            ManageTime.SpiderCount++;
        }

        rb.velocity = Vector2.zero;
        CobWebScript.Parried = false;
        anim.SetBool("Death", true);
        SpiderPreDeathAudio.Play();

        yield return new WaitForSeconds(0.5f);
        GetComponent<DistanceFadeScript>().enabled = false;
        SpiderDeathAudio.Play();
        sp.color = new Color(1f, 1f, 1f, 0f);
        SpiderDeathParticles.Play();

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cobweb") && CobWebScript.Parried == true)
        {
            StartCoroutine(DeathTimer());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            CanFollow = true;
            FollowTrigger.enabled = false;
        }

        if(collision.gameObject.CompareTag("AirTrap") && FireGunScript.FiredUp == true)
        {
            StartCoroutine(DeathTimerFire());
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 targetVelocity = Vector2.zero;

        if (distance > 3f)
        {
            targetVelocity = direction * Speed;
        }
        else if (distance <= 3f)
        {
            targetVelocity = Vector2.zero;
        }

        float smoothTime = (targetVelocity.magnitude > currentVelocity.magnitude) ? (1f / acceleration) : (1f / deceleration);
        currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime / smoothTime);
        rb.velocity = currentVelocity;


        if (transform.position.x - Player.transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }

        if (distance <= 4f && distance >= 1f)
        {
            if (!AttackCooldown) StartCoroutine(SpitAttack());
        }
    }

    private IEnumerator DeathTimerFire()
    {
        if (!AddedCount)
        {
            AddedCount = true;
            ManageTime.SpiderCount++;
        }

        StopCoroutine(SpitAttack());
        rb.velocity = Vector2.zero;
        Speed = 0f;
        GetComponent<DistanceFadeScript>().enabled = false;
        OnFlame.Play();
        BurnSound.Play();
        yield return new WaitForSeconds(0.6f);
        OnFlame.Stop();
        sp.color = Color.black;
        yield return new WaitForSeconds(1f);
        sp.color = new Color(1f, 1f, 1f, 0f);
        DeathAshes.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
