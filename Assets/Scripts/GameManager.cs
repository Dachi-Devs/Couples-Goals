using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<Objective> objectiveList;

    [SerializeField]
    private Transform respawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        objectiveList = new List<Objective>();        
        //Cursor.visible = false;
    }

    private void Start()
    {
        CheckpointManager.CheckpointUpdateEvent.AddListener(UpdateRespawnPoint);
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

    public void RespawnPlayer(GameObject playerDead)
    {
        playerDead.transform.position = respawnPoint.position;
    }

    private void UpdateRespawnPoint(Checkpoint point)
    {
        if (respawnPoint != point.transform)
        {
            respawnPoint = point.transform;
            point.PlayParticles();
        }
    }
}
