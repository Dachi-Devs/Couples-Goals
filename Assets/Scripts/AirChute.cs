using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirChute : MonoBehaviour
{
    [SerializeField]
    private float airForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb2d = collision.transform.root.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity += Vector2.up * airForce * 10f;
        }
    }
}
