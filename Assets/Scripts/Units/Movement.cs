using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float xMovement;
    private Rigidbody2D rb2d;

    private Transform sprites;
    private bool faceRight = true;

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

    public void SetVelocity(float xMovement)
    {
        this.xMovement = xMovement;
    }

    public void SetVelocity(Vector3 target)
    {
        if (target.x < transform.position.x)
            xMovement = -1f;
        else
            xMovement = 1f;
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(xMovement * moveSpeed, rb2d.velocity.y);
        CheckSprite();
        //PlayAnimWhenMade
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
        Vector3 scale = sprites.localScale;
        scale.x *= -1;
        sprites.localScale = scale;
    }

}
