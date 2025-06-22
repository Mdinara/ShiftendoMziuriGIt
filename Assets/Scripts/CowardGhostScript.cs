using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CowardGhostScript : MonoBehaviour
{
    public float Speed = 3f;

    Rigidbody2D rb;
    SpriteRenderer sp;
    Animator anim;

    bool MovingRight;

    bool MovingUp;

    bool Resting = false;

    bool BeingDestroyed;

    public GameObject DestroyAnim;

    bool AddedCount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   

        StartCoroutine(ChangeDirectionTimer());
    }

    void Update()
    {
        if (rb.velocity == Vector2.zero && BeingDestroyed)
        {
            anim.SetBool("Moving", false);
            anim.SetBool("Destroyed", true);
        }

        if (rb.velocity == Vector2.zero)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AirTrap") && Resting)
        {
            StartCoroutine(DestroyTimer());
        }

        if (collision.gameObject.CompareTag("UpFence"))
        {
            rb.velocity = new Vector2 (rb.velocity.x, -Speed);
        }
        if (collision.gameObject.CompareTag("DownFence"))
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
        }
        if (collision.gameObject.CompareTag("RightFence"))
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            rb.transform.localScale = new Vector3(4.4f, 4.4f, 4.4f);
        }
        if (collision.gameObject.CompareTag("LeftFence"))
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            rb.transform.localScale = new Vector3(-4.4f, 4.4f, 4.4f);
        }

        if (!Resting && collision.gameObject.CompareTag("AirTrap"))
        {
            StartCoroutine(SpeedBoost());
            StartMovement();
        }

        if(collision.gameObject.tag == "GroundTrapCol")
        {
            if (MovingRight) LeftMovement();
            if (!MovingRight) RightMovement();
            if(MovingUp) DownMovement();
            if(!MovingUp) UpMovement();
        }
    }

    void StartMovement()
    {
        int RandomDirection = Random.Range(1, 9);
        if (RandomDirection == 1) RightMovement();
        if (RandomDirection == 2) LeftMovement();
        if (RandomDirection == 3) UpMovement();
        if (RandomDirection == 4) DownMovement();
        if (RandomDirection == 5) RightUpMovement();
        if (RandomDirection == 6) LeftUpMovement();
        if (RandomDirection == 7) RightDownMovement();
        if (RandomDirection == 8) LeftDownMovement();
    }

    private IEnumerator ChangeDirectionTimer()
    {
        while (true)
        {
            Resting = false;

            float moveTime = 10f;
            float timer = 0f;

            while (timer < moveTime)
            {
                StartMovement();
                float wait = Random.Range(2f, 5f); 
                yield return new WaitForSeconds(wait);
                timer += wait;
            }

            Resting = true;
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(Random.Range(4f, 5f));
        }
    }

    private IEnumerator SpeedBoost()
    {
        Speed += 1;
        yield return new WaitForSeconds(3);
        Speed -= 1;
    }

    private IEnumerator DestroyTimer()
    {
        if (!AddedCount)
        {
            AddedCount = true;
            ManageTime.CowardCount++;
        }
        BeingDestroyed = true;
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    void RightMovement()
    {
        rb.velocity = new Vector2(Speed, 0);
        rb.transform.localScale = new Vector3(-4.4f, 4.4f, 4.4f);
        MovingRight = true;
    }
    void LeftMovement()
    {
        rb.velocity = new Vector2(-Speed, 0);
        rb.transform.localScale = new Vector3(4.4f, 4.4f, 4.4f);
        MovingRight = false;
    }
    void UpMovement()
    {
        rb.velocity = new Vector2(0, Speed);
        MovingUp = true;
    }
    void DownMovement()
    {
        rb.velocity = new Vector2(0, -Speed);
        MovingUp = true;
    }
    void RightUpMovement()
    {
        rb.velocity = new Vector2(Speed, Speed);
        rb.transform.localScale = new Vector3(-4.4f, 4.4f, 4.4f);
        MovingRight = true;
    }
    void LeftUpMovement()
    {
        rb.velocity = new Vector2(-Speed, Speed);
        rb.transform.localScale = new Vector3(4.4f, 4.4f, 4.4f);
        MovingRight = false;
    }
    void RightDownMovement()
    {
        rb.velocity = new Vector2(Speed, -Speed);
        rb.transform.localScale = new Vector3(-4.4f, 4.4f, 4.4f);
        MovingRight = true;
    }
    void LeftDownMovement()
    {
        rb.velocity = new Vector2(-Speed, -Speed);
        rb.transform.localScale = new Vector3(4.4f, 4.4f, 4.4f);
        MovingRight = false;
    }

}