using UnityEngine;

public class EmenyComtroller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float maxHP = 3;
    [SerializeField] private float damage = 1;
    private float currentHp;
    void Start()
    {
        currentHp = maxHP;
        target = GameObject.Find("Player").transform;
    }

    void OnEnable()
    {
        speed = 1;
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
        if (diff.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void TakeDamage(int damage)
    {
    }
}
