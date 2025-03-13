using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    private Animator _animator;
    private float currentTime;
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

        if (Input.GetKey(KeyCode.Space) && isSword)
        {
            _animator.SetBool("isAttack", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.SetBool("isAttack", false);
        }
    }
}
