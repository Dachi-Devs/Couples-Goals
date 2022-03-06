using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform[] targets;
    private int targetIndex = 0;

    private float range = 10f;

    private enum MovementType
    {
        patrol,
        direct,
        flyingPatrol,
        flyingDirect,
        stationary
    }

    [SerializeField]
    private MovementType movementType;

    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        switch (movementType)
        {
            case MovementType.patrol:
                MoveToTarget();
                break;
            case MovementType.direct:

                break;
            case MovementType.flyingPatrol:
                break;
            case MovementType.flyingDirect:
                break;
            case MovementType.stationary:
                break;
        }
    }

    private void MoveToTarget()
    {
        movement.SetVelocity(targets[targetIndex].position);
        if (Mathf.Abs(transform.position.x - targets[targetIndex].position.x) < 0.02f)
        {
            if (targetIndex >= targets.Length - 1)
                targetIndex = 0;
            else
                targetIndex++;
            movement.IdleMovement(2f);
        }
    }

    private Transform ClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Transform closestPlayer = players[0].transform;

        float dist = Vector3.Distance(transform.position, closestPlayer.position);

        foreach (GameObject p in players)
        {
            if (IsCloserThanOther(dist, p.transform))
            {
                closestPlayer = p.transform;
                dist = Vector3.Distance(transform.position, closestPlayer.position);
            }
        }

        return closestPlayer;
    }

    private bool IsCloserThanOther(float distance, Transform other)
    {
        float otherDist = Vector3.Distance(transform.position, other.position);

        return otherDist > distance;
    }
}