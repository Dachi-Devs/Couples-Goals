using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRequest : MonoBehaviour
{
    [SerializeField]
    private Objective[] itemsToRequest;

    [SerializeField]
    private GameObject thoughtBubble;
    [SerializeField]
    private Transform thoughtPos;

    private GameObject bubble;

    [SerializeField]
    private GameObject item;

    private List<Collider2D> colliders = new List<Collider2D>();

    public event EventHandler OnFulfilled; 

    public bool CheckItems()
    {
        foreach (Objective item in itemsToRequest)
        {
            if (!FindObjectOfType<GameManager>().hasObjective(item))
            {
                return false;
            }

        }
        return true;
    }

    private void DisplayRequests()
    {
        bubble = Instantiate(thoughtBubble, thoughtPos);
        foreach (Objective item in itemsToRequest)
        {
            bubble.GetComponent<ThoughtBubble>().SetItemToDisplay(item);
            StartCoroutine(ItemDisplayTime(10f / itemsToRequest.Length));
        }
    }

    public void HideRequests()
    {
        Destroy(bubble);
    }

    private void RequestFulfilled()
    {
        Destroy(GetComponent<BoxCollider2D>());
        item.GetComponent<ObjectiveWorld>().SetItem(itemsToRequest[0]);
        item.gameObject.SetActive(true);
        GetComponent<Animator>().SetTrigger("Fulfilled");
        OnFulfilled?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator ItemDisplayTime(float timePerItem)
    {
        yield return new WaitForSeconds(timePerItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!colliders.Contains(collision))
            {
                colliders.Add(collision); 
                if (CheckItems())
                {
                    RequestFulfilled();
                    return;
                }
                if (colliders.Count == 1)
                {
                    DisplayRequests();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {            
        if (collision.gameObject.tag == "Player")
        {
            colliders.Remove(collision);
            if (colliders.Count == 0)
            {
                HideRequests();
            }
        }
    }
}
