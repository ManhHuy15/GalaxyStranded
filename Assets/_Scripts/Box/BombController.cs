using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float blastRadius = 2f;
    public float timeDelay = 2f;
    public Animator _animator;

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

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
