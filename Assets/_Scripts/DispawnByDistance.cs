using UnityEngine;

public class DispawnByDistance : MonoBehaviour
{
    [SerializeField] private float distanceLimit = 25f;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.mainCamera = Transform.FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        DespawnByDistance();
    }

    public void DespawnByDistance()
    {
        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        if (distance < distanceLimit) return;
        gameObject.SetActive(false);
    }
}
