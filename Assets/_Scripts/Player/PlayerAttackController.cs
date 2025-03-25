using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    private Animator _animator;
    private float currentTime;
    [SerializeField] private bool isSword;
    [SerializeField] private GameObject hitbox;


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

    public void StartAttack()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Sword);
        hitbox.SetActive(true);
    }

    public void StopAttack()
    {
        hitbox.SetActive(false);
    }

 
}
