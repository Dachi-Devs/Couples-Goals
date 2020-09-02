using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Objective> objectiveList;

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    [SerializeField]
    private GameObject endPortal;

    private void Awake()
    {
        objectiveList = new List<Objective>();
    }

    private void Start()
    {
        Cursor.visible = false;
        FindObjectOfType<ItemRequest>().OnFulfilled += ItemRequest_OnFulfilled;
    }

    private void ItemRequest_OnFulfilled(object sender, EventArgs e)
    {
        SpawnPortal();
    }

    public void AddObjective(Objective objective)
    {
        objectiveList.Add(objective);
    }

    public void RemoveObjective(Objective objective)
    {
        objectiveList.Remove(objective);
    }
    
    public bool hasObjective(Objective objective)
    {
        return objectiveList.Contains(objective);
    }

    public void KillUnit(GameObject objectToKill)
    {
        Destroy(objectToKill);
    }

    public void RespawnPlayer(GameObject playerDead)
    {
        playerDead.transform.position = startPoint.position;
    }

    private void SpawnPortal()
    {
        Instantiate(endPortal, endPoint.position, Quaternion.identity);
    }
}
