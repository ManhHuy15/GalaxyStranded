using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float blastRadius = 0.5f;
    public float timeDelay = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Debug.Log("Bomb blast!");

        // Tạo một vùng ảnh hưởng xung quanh vị trí của bomb
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Enemy"))
            {
                Debug.Log($"{collider.name} bị ảnh hưởng bởi vụ nổ!");
                // Gọi hàm gây sát thương nếu cần
                // collider.GetComponent<Health>()?.TakeDamage(damage);
            }
        }

        // Hiển thị hiệu ứng nổ (nếu có)
        // Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ bán kính vụ nổ để dễ debug
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
