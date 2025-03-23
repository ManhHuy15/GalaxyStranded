using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Camera mainCamera;
    float distanceLimit = 15f;
    private float knockBackForce = 3f;
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
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerController>().TakeDame(10);
        }
        else if (gameObject.name == "Arrow" && collision.gameObject.tag == "Enemy")
        {
            Vector3 parentPos = transform.parent.position;

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();

            Vector2 force = direction * knockBackForce;
            gameObject.SetActive(false);

            collision.gameObject.GetComponent<EmenyComtroller>().TakeDame(1, force);
        }

        
    }
}
