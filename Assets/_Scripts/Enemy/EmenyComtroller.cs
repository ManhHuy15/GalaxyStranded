using UnityEngine;

public class EmenyComtroller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxHP = 3f;
    [SerializeField] private float avoidRadius = 0.4f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isSlime;
    [SerializeField] private float stopDistance = 5f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private GameObject ballPrefab;
    
    private float currentTime = 0f;
    private float currentHp;
    private float distanceToPlayer;
    private Animator _animator;

    void Start()
    {
        currentHp = maxHP;
        target = GameObject.Find("Player").transform;
        _animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        speed = 1f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        FollowTarget();
        Attack();
    }

    private void FollowTarget()
    {
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        Vector2 currentPos = transform.position;
        Vector2 moveDirection = (targetPos - currentPos).normalized;
        distanceToPlayer = Vector2.Distance(currentPos, targetPos);


        if (distanceToPlayer > stopDistance)
        {
            moveDirection = (targetPos - currentPos).normalized; 
        }
        else if (isSlime && distanceToPlayer <= stopDistance)
        {
            moveDirection = (currentPos - targetPos).normalized; 
        }

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidRadius, enemyLayer);
        if (nearbyEnemies.Length > 1)
        {
            Vector2 avoidDirection = Vector2.zero;
            foreach (Collider2D enemy in nearbyEnemies)
            {
                if (enemy.gameObject != gameObject)
                {
                    Vector2 awayFromEnemy = (transform.position - enemy.transform.position).normalized;
                    avoidDirection += awayFromEnemy;
                }
            }
            moveDirection = (moveDirection + avoidDirection.normalized).normalized;
        }

        if (moveDirection != Vector2.zero)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPos + moveDirection * speed * Time.deltaTime, speed * Time.deltaTime);
        }

        if (targetPos.x < currentPos.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, avoidRadius);

        if (isSlime)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stopDistance);
        }
    }


    void Attack()
    {

        if (isSlime && currentTime >= attackInterval && distanceToPlayer <= stopDistance)
        {
            _animator.SetBool("isAttack", true);
            currentTime = 0;
        }
        else
        {
            _animator.SetBool("isAttack", false);
        }
    }

    void SpawnBall()
    {
        Vector3 spawnOffset = transform.forward * 0.5f;
        Vector3 pos = transform.position + spawnOffset;
        pos.z = -1f;

        Vector2 direct = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        SpawnerManager.Instance.SpawnObject(ballPrefab, pos, Quaternion.Euler(0, 0, angle));
    }
}
