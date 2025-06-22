using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackScript : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject Player;

    public float Speed;

    bool StopFollow;

    Vector2 directionSet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance == 1f && !StopFollow)
        {
            StopFollow = true;
            directionSet = (Player.transform.position - transform.position).normalized;
        }

        if (!StopFollow)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;

            rb.velocity = direction * Speed;
        }
        else
        {

            rb.velocity = directionSet * Speed;
        }
    }
}
