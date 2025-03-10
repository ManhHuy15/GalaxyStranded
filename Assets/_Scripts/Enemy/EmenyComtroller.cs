using UnityEngine;

public class EmenyComtroller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float maxHP = 3;
    private float currentHp;
    void Start()
    {
        currentHp = maxHP;
    }

    void OnEnable()
    {
        speed = UnityEngine.Random.Range(1, 3);
    }
    void Update()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed*Time.deltaTime);

        Vector3 diff = target.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    public void TakeDamage(int damage)
    {
    }
}
