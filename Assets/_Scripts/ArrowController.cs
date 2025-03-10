using UnityEngine;

public class ArrowController : MonoBehaviour
{

    private Camera mainCamera;

    private float speed = 10f;
    float distanceLimit = 15f;
    void Start()
    {
        this.mainCamera = Transform.FindAnyObjectByType<Camera>();
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        DespawnByDistance();
    }

    public void DespawnByDistance()
    {
        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        if (distance < distanceLimit) return;
        gameObject.SetActive(false);
    }
}
