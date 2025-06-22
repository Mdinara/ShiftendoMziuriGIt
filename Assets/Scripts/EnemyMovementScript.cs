using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float Speed;
    public int patrolDestination;


    private void Update()
    {
        if(patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, Speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                patrolDestination = 0;
            }
        }
    }
}
