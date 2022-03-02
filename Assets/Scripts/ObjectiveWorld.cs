using UnityEngine;

public class ObjectiveWorld : MonoBehaviour
{
    [SerializeField]
    private Objective obj;

    private bool isCollected = false;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        SpriteRenderer spr = GetComponentInChildren<SpriteRenderer>();
        spr.sprite = obj.objectiveSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isCollected)
                return;
            isCollected = true;
            FindObjectOfType<GameManager>().AddObjective(obj);
            Destroy(gameObject);
        }
    }

    public void SetItem(Objective item)
    {
        obj = item;
        Setup();
    }
}
