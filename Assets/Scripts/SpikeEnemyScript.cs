using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemyScript : MonoBehaviour
{
    public GameObject Player;

    public static GameObject BiteHitboxPlayer;

    public GameObject BiteHitbox;

    Animator anim;

    bool Attacked;

    public AudioSource BiteSound;

    SpriteRenderer sp;

    public ParticleSystem DeathAshes;

    public GameObject Spikes;

    public SpriteRenderer SpikesSp;

    bool Dying;

    public ParticleSystem OnFlame;

    public AudioSource BurnSound;
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < 2.5f && !Dying) LookAtPlayer();

        if (distance < 2f && !Attacked && !Dying) StartCoroutine(AttackPlayer());

        BiteHitboxPlayer = BiteHitbox.gameObject;
    }

    void LookAtPlayer()
    {
        if (transform.position.x - Player.transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }
    }

    private IEnumerator AttackPlayer()
    {
        Attacked = true;
        anim.SetBool("Attacked", true);

        yield return new WaitForSeconds(0.3f);
        BiteSound.Play();
        BiteHitbox.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        BiteHitbox.SetActive(false);

        yield return new WaitForSeconds(0.38f);
        anim.SetBool("Attacked", false);

        yield return new WaitForSeconds(3f);
        Attacked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("AirTrap") && FireGunScript.FiredUp == true)
        {
            StartCoroutine(DeathTimer());
        }
    }

    private IEnumerator DeathTimer()
    {
        Dying = true;
        OnFlame.Play();
        BurnSound.Play();
        yield return new WaitForSeconds(0.6f);
        OnFlame.Stop();
        sp.color = Color.black;
        SpikesSp.color = Color.black;
        yield return new WaitForSeconds(1f);
        sp.color = new Color(1f, 1f, 1f, 0f);
        SpikesSp.color = new Color(1f, 1f, 1f, 0f);
        DeathAshes.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(Spikes);
    }
}
