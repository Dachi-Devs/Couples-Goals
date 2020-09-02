using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 posToFollow;

    [SerializeField]
    private float speed;

    [SerializeField]
    private CamTargetMode targetMode;

    private Camera cam;

    public enum CamTargetMode
    {
        playersFollow,
        targetFollow,
        cutscene
    }

    [SerializeField]
    private Transform[] targetTransforms;

    void Start()
    {
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

        midpoint /= targetTransforms.Length;

        return midpoint;
    }

    private float CalculateSize()
    {
        float distance = Vector3.Distance(transform.position, targetTransforms[0].position);

        return distance * 0.65f;
    }

    private void FollowTarget()
    {
            Vector3 camMove = Vector3.Lerp(transform.position, posToFollow, speed * Time.deltaTime);

            transform.position = new Vector3(camMove.x, camMove.y, -10f);
    }
}
