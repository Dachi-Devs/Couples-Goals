using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().RespawnPlayer(collision.gameObject);
        }
    }
}
