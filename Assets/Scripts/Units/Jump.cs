using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    public void DoJump()
    {
        rb2d.velocity = Vector3.up * jumpForce;
    }

    public void DoJump(float jumpMultiplier)
    {
        rb2d.velocity = Vector3.up * jumpForce * jumpMultiplier;
    }
}
