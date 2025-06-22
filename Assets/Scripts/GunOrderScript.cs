using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOrderScript : MonoBehaviour
{
    SpriteRenderer sp;

    Rigidbody2D rb;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (PlayerMovementScript.ChangeGunsOrder == true)
            sp.sortingLayerName = "GunBehind";
        else
        {
            sp.sortingLayerName = "Default";
            sp.sortingOrder = 0;
        }
    }
}
