using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float xMovement;
    private Rigidbody2D rb2d;

    [SerializeField]
    private float jumpForce = 1f;
    private const float jumpMult = 10f;
    private const float moveMult = 100f;

    [SerializeField]
    private LayerMask floorLayer;

    public UnityEvent activeMovementEvent;

    private Transform sprites;
    private bool faceRight = true;
    private bool isFlipping = false;
    private MovementState moveState = MovementState.active;


    void Awake()
    {
        if (activeMovementEvent == null)
            activeMovementEvent = new UnityEvent();

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
        if (moveState == MovementState.active)
        {
            rb2d.AddForce(new Vector2(xMovement * moveSpeed * moveMult, 0f));
        }

        if (moveState != MovementState.locked)
        {
            if (rb2d.velocity.y < 3f)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y;
            }
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
        Collider2D floorColl = Physics2D.OverlapBox(new Vector2(feet.bounds.center.x, feet.bounds.min.y), new Vector2(feet.bounds.size.x, -0.2f), 0f, floorLayer);
        return floorColl != null;
    }

    public void DoJump()
    {
        switch (moveState)
        {
            case MovementState.active:
                if (IsGrounded())
                    rb2d.velocity += Vector2.up * jumpForce * jumpMult;
                break;
            case MovementState.locked:
                moveState = MovementState.active;
                activeMovementEvent.Invoke();
                rb2d.velocity += Vector2.up * jumpForce * jumpMult;
                break;
            case MovementState.inactive:
                break;
        }
    }

    public void DoFall()
    {
        if (moveState == MovementState.locked)
        {
            moveState = MovementState.active;
            activeMovementEvent.Invoke();
        }
    }

    public void Bounce(float jumpMultiplier)
    {
        rb2d.velocity += Vector2.up * jumpForce * jumpMultiplier * jumpMult;
    }

    public void PushInDirection(Vector2 dir)
    {
        rb2d.velocity += dir * jumpForce * jumpMult;
    }

    public void SetMovementState(MovementState state)
    {
        moveState = state;
    }
}

public enum MovementState
{
    active,
    locked,
    inactive
}
