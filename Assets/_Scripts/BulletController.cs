using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Camera mainCamera;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.name == "Ball" &&  collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Arrow" && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            gameObject.SetActive(false);
        }

        
    }
}
