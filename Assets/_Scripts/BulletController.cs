using UnityEngine;
using UnityEngine.UIElements;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private GameObject arrowDropPrefab;
    private float knockBackForce = 3f;
    void Start()
    {
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.name == "Ball" &&  collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerController>().TakeDame(10);
        }
        else if (gameObject.name == "Arrow" && collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 parentPos = transform.parent.position;

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();

            Vector2 force = direction * knockBackForce;
            gameObject.SetActive(false);

            collision.gameObject.GetComponent<EmenyComtroller>().TakeDame(2f, force);
        }
        else if (gameObject.name == "Arrow" && collision.gameObject.CompareTag("Box"))
        {
            gameObject.SetActive(false);
        }else if(collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
            if(gameObject.name == "Arrow")
            {
                SpawnerManager.Instance.SpawnObject(arrowDropPrefab, transform.position, transform.localRotation);
            }
        }

        
    }
}
