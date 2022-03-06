using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float xMovement;
    private Rigidbody2D rb2d;

    [SerializeField]
    private float jumpForce = 1f;

    [SerializeField]
    private LayerMask floorLayer;

    private Transform sprites;
    private bool faceRight = true;
    private bool isFlipping = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }

        sprites = transform.Find("Sprites");
        if (sprites == null)
            Debug.Log("NO SPRITES FOUND");
    }

    public void SetVelocity(float xMove)
    {
        xMovement = xMove;
    }

    public void SetVelocity(Vector3 target)
    {
        xMovement = target.x < transform.position.x ? -1f : 1f;
    }

    private void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(xMovement * moveSpeed, 0f));

        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y / 4f;
        }
    }

    private void Update()
    {
        CheckSprite();
        if (isFlipping)
        {
            float dir = faceRight ? 1f : -1f;
            sprites.localScale = new Vector3(Mathf.Lerp(sprites.localScale.x, dir, Time.deltaTime * 15f), sprites.localScale.y);
        }
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void IdleMovement(float waitTime)
    {
        StartCoroutine(Idle(waitTime));
    }

    private IEnumerator Idle(float waitTime)
    {
        float moveSpeedTemp = moveSpeed;
        moveSpeed = 0f;
        yield return new WaitForSeconds(waitTime);
        moveSpeed = moveSpeedTemp;
    }

    private void CheckSprite()
    {
        if (xMovement < -0.1f && faceRight == true)
            FlipSprite();
        else if (xMovement > 0.1f && faceRight == false)
            FlipSprite();
    }

    private void FlipSprite()
    {
        faceRight = !faceRight;
        isFlipping = true;
    }

    private bool IsGrounded()
    {
        CircleCollider2D feet = GetComponent<CircleCollider2D>();
        Collider2D floorColl = Physics2D.OverlapBox(new Vector2(feet.bounds.center.x, feet.bounds.min.y), new Vector2(feet.bounds.size.x, -0.1f), 0f, floorLayer);
        return floorColl != null;
    }

    public void DoJump()
    {
        if (IsGrounded())
            rb2d.velocity += Vector2.up * jumpForce;
    }

    public void Bounce(float jumpMultiplier)
    {
        rb2d.velocity += Vector2.up * jumpForce * jumpMultiplier;
    }

    public void PushInDirection(Vector2 dir)
    {
        rb2d.velocity += dir * jumpForce;
    }
}
