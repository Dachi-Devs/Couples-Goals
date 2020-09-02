using UnityEngine;

public class EndLevelPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<LevelManager>().BackToMainWorld();
        }
    }
}
