using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            GameManager.instance.RespawnPlayer(collision.gameObject);
        }

        else
        {
            Destroy(collision.gameObject);
        }
    }
}
