using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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

    private void Start()
    {
        CameraController.playersDistanceEvent.AddListener(PushTowardsPlayer);
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

        movement.SetVelocity(xMovement);
    }

    private void OnJump()
    {
        movement.DoJump();
    }

    private void OnFall()
    {
        movement.DoFall();
    }

    private void OnDebug()
    {
        PushTowardsPlayer();
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
                movement.Bounce(0.5f);
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

    private void PushTowardsPlayer()
    {
        print("PUSH");
        Transform targetPlayer = GetNextPlayer().transform;
        Vector2 dir = targetPlayer.position - transform.position;
        movement.PushInDirection(dir.normalized);
    }

    private GameObject GetNextPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] == gameObject)
            {
                if (i + 1 < players.Length)
                    return players[i + 1];

                else
                    return players[0];
            }
        }

        return players[0];
    }
}
