using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public List<Rigidbody2D> entities = new List<Rigidbody2D>();

    void FixedUpdate()
    {
        foreach (Rigidbody2D rb in entities)
        {
            rb.velocity = transform.right * 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddObject(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveObject(collision.gameObject);
    }

    private void AddObject(GameObject obj)
    {
        Movement move = obj.GetComponent<Movement>();
        if (move != null)
        {
            move.SetMovementState(MovementState.locked);
            move.activeMovementEvent.AddListener(() => RemoveObject(obj));
        }

        Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        entities.Add(rb2d);
    }

    private void RemoveObject(GameObject obj)
    {
        Movement move = obj.GetComponent<Movement>();
        if (move != null)
        {
            move.SetMovementState(MovementState.active);
            move.activeMovementEvent.RemoveListener(() => RemoveObject(obj));
        }

        Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 1f;
        entities.Remove(rb2d);
    }
}
