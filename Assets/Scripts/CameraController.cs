using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    private Vector3 posToFollow;

    [SerializeField]
    private float speed;

    [SerializeField]
    private CamTargetMode targetMode;

    [SerializeField]
    private float maxDistance;

    private bool isMaxDistance = false;
    public static UnityEvent playersDistanceEvent;

    private Camera cam;

    public enum CamTargetMode
    {
        playersFollow,
        targetFollow,
        cutscene
    }

    [SerializeField]
    private List<Transform> targetTransforms = new List<Transform>();

    private void Awake()
    {
        if (playersDistanceEvent == null)
            playersDistanceEvent = new UnityEvent(); 
    }

    void Start()
    {
        GetPlayers();
        cam = Camera.main;
    }

    private void Update()
    {
        switch (targetMode)
        {
            case CamTargetMode.playersFollow:
                posToFollow = CalculateMidpoint();
                cam.orthographicSize = CalculateSize();
                break;
            case CamTargetMode.targetFollow:
                posToFollow = targetTransforms[0].position;
                cam.orthographicSize = 3;
                break;
            case CamTargetMode.cutscene:
                break;
        }
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private Vector2 CalculateMidpoint()
    {
        Vector2 midpoint = Vector2.zero;
        foreach (Transform transform in targetTransforms)
        {
            midpoint += (Vector2)transform.position;
        }

        midpoint /= targetTransforms.Count;

        Vector2 offset = new Vector2(0f, 5f);

        midpoint += offset;

        return midpoint;
    }

    private void GetPlayers()
    {
        targetTransforms.Clear();

        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            targetTransforms.Add(player.transform);
        }
    }

    private float CalculateSize()
    {
        float distanceX = Mathf.Abs(transform.position.x - targetTransforms[0].position.x);
        float distanceY = Mathf.Abs(transform.position.y - targetTransforms[0].position.y);

        float distance = distanceX;

        if (distanceY > distanceX)
            distance = distanceY * 16f / 9f;

        if (distance > maxDistance)
        {
            if (!isMaxDistance)
                StartCoroutine(PushbackDelay());
        }

        float size = Mathf.Clamp(distance * 0.8f, 24f, 40f);

        return size;
    }

    private void FollowTarget()
    {
            Vector3 camMove = Vector3.Lerp(transform.position, posToFollow, speed * Time.deltaTime);

            transform.position = new Vector3(camMove.x, camMove.y, -10f);
    }

    private IEnumerator PushbackDelay()
    {
        isMaxDistance = true;
        playersDistanceEvent.Invoke();

        yield return new WaitForSeconds(0.5f);

        isMaxDistance = false;
    }
}
