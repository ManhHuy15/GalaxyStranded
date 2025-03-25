using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 velocity = Vector3.zero;

    [Range(0f, 1f)]
    public float smoothTime = 0.3f;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Reset()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        UpdatePos();
    }

    private void LateUpdate()
    {
        UpdatePos();
    }


    private void UpdatePos()
    {
        Vector3 targetPos = target.position;
        targetPos = new Vector3(targetPos.x, targetPos.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
