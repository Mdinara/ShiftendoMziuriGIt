using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.U2D;

public class DistanceFadeScript : MonoBehaviour
{
    public GameObject Player;  

    SpriteRenderer sp;

    public float maxDistance = 5f;
    public float minAlpha = 0.1f;

    public bool reverse = false;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        Player = GameObject.Find("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(
        new Vector2(Player.transform.position.x, Player.transform.position.y),
        new Vector2(transform.position.x, transform.position.y));

        float t = Mathf.Clamp01(distance / maxDistance);

        float f = Mathf.Clamp01(maxDistance / distance);

        float alpha;
        if (!reverse)
        {
            alpha = Mathf.Lerp(1f, minAlpha, t);
        }
        else
        {
            alpha = Mathf.Lerp(1f, minAlpha, f);
        }


        Color color = sp.color;
        color.a = alpha;
        sp.color = color;
    }
}
