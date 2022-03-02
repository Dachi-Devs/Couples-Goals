using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLayer;
    [SerializeField]
    private LayerMask enemyLayer;

    private float xMovement;

    private Movement movement;
    private Animator anim;


    private bool isCollided = false;
    private void Awake()
    {
        movement = GetComponent<Movement>();
        anim = GetComponent<Animator>();
    }

    private void OnMove(InputValue value)
    {
        xMovement = value.Get<float>();
        if (xMovement == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
            anim.SetBool("IsRunning", true);
    }

    private void OnJump()
    {
        if (IsGrounded())
            GetComponent<Jump>().DoJump();
    }

    private void Update()
    {
        movement.SetVelocity(xMovement);
    }

    private bool IsGrounded()
    {
        CircleCollider2D feet = GetComponent<CircleCollider2D>();
        Collider2D floorColl = Physics2D.OverlapBox(new Vector2(feet.bounds.center.x, feet.bounds.min.y), new Vector2(feet.bounds.size.x, -0.2f), 0f, floorLayer);
        return floorColl != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (isCollided == true)
                return;
            CircleCollider2D feet = GetComponent<CircleCollider2D>();
            Collider2D enemyColl = Physics2D.OverlapBox(new Vector2(feet.bounds.center.x, feet.bounds.min.y), new Vector2(feet.bounds.size.x * 0.8f, 0.4f), 0f, enemyLayer);

            if (enemyColl != null)
            {
                Destroy(enemyColl.transform.root.gameObject);
                GetComponent<Jump>().DoJump(0.5f);
            }
            else
            {
                FindObjectOfType<GameManager>().RespawnPlayer(gameObject);
            }
            StartCoroutine(pauseCollisions());
        }
    }

    private IEnumerator pauseCollisions()
    {
        isCollided = true;
        yield return new WaitForFixedUpdate();
        isCollided = false;
    }
}
