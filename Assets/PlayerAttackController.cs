using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.Space)) { 
            _animator.SetBool("isAttack", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.SetBool("isAttack", false);
        }
    }
}
