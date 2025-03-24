using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float blastRadius = 2.5f;
    public float timeDelay = 1f;
    public Animator _animator;
    private float knockBackForce = 10f;
    private float damage = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(ExplodeAfterDelay());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdown()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(timeDelay);
        Blast();
    }

    private void Blast()
    {
        _animator.SetBool("isExplosion", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDame(damage);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 parentPos = transform.position;

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();

            Vector2 force = direction * knockBackForce;
            gameObject.SetActive(false);

            collision.gameObject.GetComponent<EmenyComtroller>().TakeDame(damage, force);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
