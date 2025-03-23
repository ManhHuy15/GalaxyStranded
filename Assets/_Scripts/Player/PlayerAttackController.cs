using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    private Animator _animator;
    private float currentTime;
    private float knockBackForce = 8f;
    [SerializeField] private bool isSword;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSword)
        {
            if (currentTime < 1f)return;
            _animator.SetBool("isAttack", true);
            currentTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isSword)
        {
            _animator.SetBool("isAttack", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.SetBool("isAttack", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 parentPos = transform.parent.position;

            Vector2 direction =   collision.gameObject.transform.position - parentPos ;
            direction.Normalize();

            Vector2 force = direction * knockBackForce;

            collision.gameObject.GetComponent<EmenyComtroller>().TakeDame(2f, force);
        }
    }
}
