using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningNightlordAttack : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 10f;

    Animator anim;



    void Start()
    {
        pivotPoint = GameObject.Find("Nightlord").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if(anim.GetBool("Dying") == true)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
