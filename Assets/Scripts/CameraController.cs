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

    private Vector3 CalculateMidpoint()
    {
        Vector3 midpoint = Vector3.zero;
        foreach (Transform transform in targetTransforms)
        {
            midpoint += transform.position;
        }

        midpoint /= targetTransforms.Count;

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
        float distance = Mathf.Abs(transform.position.x - targetTransforms[0].position.x);

        if (distance > maxDistance)
        {
            if (!isMaxDistance)
                StartCoroutine(PushbackDelay());
        }

        float size = Mathf.Clamp(distance + 2f, 6f, 12f);

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

        yield return new WaitForSeconds(0.3f);

        isMaxDistance = false;
    }
}
